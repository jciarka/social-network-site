import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import "./Chat.css";
import { useSelector, useDispatch } from "react-redux";
import { Link, useParams } from "react-router-dom";
import { BsFillArrowDownCircleFill } from "react-icons/bs";

const Chat = ({ onSuccess = null }) => {
    let { chatId } = useParams();
    const [chat, setChat] = useState([]);
    const [entry, setEntry] = useState([]);
    const [entries, setEntries] = useState([]);
    const [member, setMember] = useState([]);
    const [backendErrors, setBackendErrors] = useState([]);
    
    const account = useSelector((state) => state.account);
    const messageContainer = useRef(null);

    const fetchChats = async () => {
        let fetchedChat, fetchedMember, updatedDate;
        fetchedChat = await axios.get(`/api/chat/${chatId}`);
        fetchedMember = await axios.get(`/api/Accounts/findChatMember/${chatId}`);

        if (fetchedChat && fetchedChat.data && fetchedChat.data.success) {
          setChat({ ...fetchedChat.data.model.chat, expanded: false, detailsFetched: false });
          setMember({...fetchedMember.data.member});
        }
    };
    
      useEffect(() => {
        fetchChats();
        updateViewDate();
        getEntries();
        setInterval(() => {
            getEntries();
        }, 3000);
        if(messageContainer) {
            messageContainer.current.addEventListener('DOMNodeInserted', event => {
                scrollDownMessages();
            });
        }
     // eslint-disable-next-line react-hooks/exhaustive-deps
      }, []);
    
    const submit = async () => {
    try {
        const result = await axios.post("/api/chatEntry", {
            ...entry,
            chatId: chatId,
            text: entry,
        });

        if (
            result &&
            result.data &&
            result.data.success) 
        {
            setEntry([]);
        } else {
            setBackendErrors(result.data.errors);
            return;
        }

        if (onSuccess) {
            onSuccess(result.data.model);
        }
        getEntries();
    } 
    catch (e) {
        if (e.response) {
            setBackendErrors([e.message]);
        } else {
            setBackendErrors(e);
        }
    }
    };

    const getEntries = async () => {
        let result;
        result = await axios.get(`/api/chatEntry/list/chat/${chatId}`);
        setEntries(result.data.model.map((x) => {
            let d = new Date(x.postDate)
            if(account.id === x.accountId) {
                return (
                    <div className="single-entry-user">
                        <div>
                            {x.text}
                        </div>
                        <div id="entry-date-user">
                            {d.getHours().toString().padStart(2,0)}:{d.getMinutes().toString().padStart(2,0)}   {d.getDate().toString().padStart(2,0)}.{(d.getMonth()+1).toString().padStart(2,0)}.{d.getFullYear()}
                        </div>    
                    </div>
                );
            }
            else {
                return (
                    <div className="single-entry-member">
                        <div>
                            {x.text}
                        </div>
                        <div id="entry-date-member">
                            {d.getHours().toString().padStart(2,0)}:{d.getMinutes().toString().padStart(2,0)}   {d.getDate().toString().padStart(2,0)}.{(d.getMonth()+1).toString().padStart(2,0)}.{d.getFullYear()}
                        </div>                        
                    </div>
    
                );
            }
        }));
    }

    const scrollDownMessages = () => {
        if(messageContainer)
        {
            var elementHeight = messageContainer.current.scrollHeight;
            messageContainer.current.scrollTop = elementHeight;   
        }
    };

    // const visibilityState = () => {
    //     if(messageContainer) 
    //     {
    //         var elementHeight = messageContainer.current.scrollHeight;
            
    //         if(messageContainer.current.scrollTop !== elementHeight) 
    //         {
    //             return true;
    //         }

    //     }
    //     return false;
    // }

    const updateViewDate = async () => {
        let result = axios.put(`/api/ChatAccount/${chatId}`);
    }

    
    return (
        <div
        className="container d-flex justify-content-center"
        id="main-container"
        >
        <div
            className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
            style={{ border: "#8f8f8fb6" }}
        >
            <div className="text-center mb-3">
                <h4>{chat.name}</h4>
                <h6>Użytkownik: {member.firstname} {member.lastname}</h6>
            </div>

            <div ref={messageContainer} id="entries-container">
                {entries}
                {/* {visibilityState ? <BsFillArrowDownCircleFill onClick={scrollDownMessages} id="scroll-down-button"/> : null} */}
                
            </div>

            <div id="entry-group">            
                <div className="form-group" id="entry-form">
                    <label htmlFor="text">Nowa wiadomość</label>
                    <textarea
                        type="text"
                        className="form-control"
                        placeholder="Nowa wiadomość"
                        value={entry}
                        onChange={(e) => {
                        setEntry(e.target.value);
                        }}
                    />
                </div>
                <div className="w-100 text-center" id="send-button">
                    <button
                        type="submit"
                        className="btn btn-primary rounded-0"
                        onClick={(e) => submit()}
                    >
                        Wyślij
                    </button>
                </div>
            </div>
        </div>
        </div>
    );
};

export default Chat;
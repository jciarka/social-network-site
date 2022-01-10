import React, { useState, useEffect } from "react";
import axios from "axios";
import "./Chat.css";
import { useSelector, useDispatch } from "react-redux";
import { Link, useParams } from "react-router-dom";
import { chatActionCreators } from "../../store/index";
import { bindActionCreators } from "redux";

const Chat = ({ onSuccess = null }) => {
    let { chatId } = useParams();
    const [chat, setChat] = useState([]);
    const [entry, setEntry] = useState([]);
    const [entries, setEntries] = useState([]);
    const [backendErrors, setBackendErrors] = useState([]);
    
    const account = useSelector((state) => state.account);
    const entriesRefresh = useSelector((state) => state.chat);
    const dispatch = useDispatch();
    const { initChat, setRefreshChat, setUpToDateChat } = bindActionCreators(chatActionCreators, dispatch);

    const fetchChats = async () => {
        let result;
        result = await axios.get(`/api/chat/${chatId}`);
    
        if (result && result.data && result.data.success) {
          setChat({ ...result.data.model.chat, expanded: false, detailsFetched: false });
        }
      };
    
      useEffect(() => {
        fetchChats();
        getEntries();
        setInterval(() => {
            getEntries();
        }, 3000)
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
            </div>

            <div id="entries-container">
                {entries}
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
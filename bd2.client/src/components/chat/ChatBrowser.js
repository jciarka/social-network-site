import React, { useState, useEffect } from "react";
import ChatEditor, { editorTypes } from "components/chat/ChatEditor";
import axios from "axios";
import ChatCard from "components/chat/ChatCard";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

const ChatBrowser = ({}) => {
  const [chats, setChats] = useState([]);

  const account = useSelector((state) => state.account);
  let { groupId } = useParams();

  const fetchChats = async () => {
    let result;
    result = await axios.get(`/api/chat/list/user/${account.id}`);

    if (result && result.data && result.data.success) {
      console.log(result);
      setChats(
        result.data.model.map((x) => {
          return { ...x, expanded: false, detailsFetched: false };
        })
      );
    }
  };

  useEffect(() => {
    fetchChats();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getChatDataSetter = (index) => {
    return (newData) => {
      if (newData) {
        setChats(chats.map((x, i) => (i !== index ? x : newData)));
      } else {
        setChats(chats.filter((x, i) => (i !== index ? true : false)));
      }
    };
  };

  return (
    <>
      <div className="container justify-content-center">
        {
          <ChatEditor
            type={editorTypes.EDIT}
            onSuccess={(x) => fetchChats()}
            header="Stwórz nowy czat"
            post={{ groupId }}
          />
        }

        {chats.map((chatData, index) => {
          return (
            <div key={index} className="row justify-content-center">
              <ChatCard
                key={index}
                chatData={chatData}
                setChatData={getChatDataSetter(index)}              
              />
            </div>
          );
        })}
      </div>
    </>
  );
};

export default ChatBrowser;

import React, { useEffect, useState } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";
import { BiXCircle } from "react-icons/bi";

const ChatCard = ({key, chatData}) => {
  const [member, setMember] = useState([]);

  const fetchMember = async() => {
    let fetchedMember;
    fetchedMember = await axios.get(`/api/Accounts/findChatMember/${chatData.id}`);

    if (fetchedMember && fetchedMember.data && fetchedMember.data.success) {
      setMember({...fetchedMember.data.member});
    }
  };

  useEffect(() => {
    fetchMember();
  }, []);

  const deleteChat = async() => {
    let result = await axios.delete(`/api/chat/${chatData.id}`);
    var chatRemovedEvent = new CustomEvent("chatRemoved", {});
    document.dispatchEvent(chatRemovedEvent);
  }

  return (
    <div>
      <BiXCircle onClick={deleteChat} id="removeButton"/>
      <Link to={`/chats/${chatData.id}`}>
      <div
        className="container d-flex justify-content-center"
        style={{ "width": 600 }}
      >
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          <div className="text-center">
              <h5>{chatData.name}</h5>
              <h6>Użytkownik: {member.firstname} {member.lastname}</h6>
          </div>
        </div>
      </div>
      </Link>
    </div>
  );
};

export default ChatCard;
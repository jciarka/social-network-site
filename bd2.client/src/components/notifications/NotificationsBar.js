import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const NotificationsBar = () => {

  const [msgNotifications, setMsgNotifications] = useState([]);
  const [reactNotifications, setReactNotifications] = useState([]);
  const [commentNotifications, setCommentNotifications] = useState([]);

  const [msgCounts, setMsgCounts] = useState(0);
  const [reactCounts, setReactCounts] = useState(0);
  const [commentCounts, setCommentCounts] = useState(0);

  const fetchNotificationCounts = async () => {
    const result = await axios.get(`/api/Notifications/Counts`);

    console.log(JSON.stringify(result.data))

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setMsgCounts(result.data.data.messages);
      setCommentCounts(result.data.data.comments);
      setReactCounts(result.data.data.reactions);
    }
  };

  const fetchNotifications = async () => {
    // const result = await axios.get(`/api/Notifications/${account.id}`);

    // if (result && result.data && result.data.success) {
    //   console.log(result.data.data);
    //   setMsgNotifications(result.data.messages);
    //   setCommentNotifications(result.data.comments);
    //   setReactNotifications(result.data.reactNotifications);
    // }
  };
  useEffect(() => {
    fetchNotificationCounts()
  }, []);

  return (
    <>
      <div className="d-flex flex-row">
        <div className="mx-1 nav-item dropdown position-static justify-content-start">
          <button className="btn btn-sm btn-outline-dark">
            Wiadomości{" "}
            <span class="badge small badge-pill badge-danger">{msgCounts}</span>
          </button>

          {msgCounts >= 1 && (
            <div className="dropdown-content">
              <div
                className="text-center bg-dark text-white "
                style={{ height: "10px" }}
              ></div>

              {msgNotifications.map((x, i) => {
                return (
                  <>
                    <Link
                      to={`/groups/chats/${x.chatId}`}
                      className="rounded-0"
                    >
                      <div key={i}>{x.chatName}</div>
                    </Link>
                  </>
                );
              })}
            </div>
          )}
        </div>
        <div className="mx-1 nav-item dropdown position-static justify-content-start">
          <button className="btn btn-sm  btn-outline-dark">
            Reakcje{" "}
            <span class="badge small badge-pill badge-danger">
              {reactCounts}
            </span>
          </button>

          {reactCounts >= 1 && (
            <div className="dropdown-content">
              <div
                className="text-center bg-dark text-white "
                style={{ height: "10px" }}
              ></div>

              {reactNotifications.map((x, i) => {
                return (
                  <>
                    <div key={i}>{x.postTitle}</div>
                  </>
                );
              })}
            </div>
          )}
        </div>

        <div className="mx-1 nav-item dropdown position-static justify-content-start">
          <button className="btn btn-sm btn-outline-dark">
            Komentarze{" "}
            <span class="badge small badge-pill badge-danger">
              {commentCounts}
            </span>
          </button>

          {commentCounts >= 1 && (
            <div className="dropdown-content">
              <div
                className="text-center bg-dark text-white "
                style={{ height: "10px" }}
              ></div>

              {commentNotifications.map((x, i) => {
                return (
                  <>
                    <div key={i}>{x.postTitle}</div>
                  </>
                );
              })}
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default NotificationsBar;

import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useLocation } from 'react-router-dom'
import axios from "axios";
import Dialog from "@mui/material/Dialog";

import PostCard from "components/post/PostCard";

const NotificationsBar = () => {
  const location = useLocation();

  const [msgNotifications, setMsgNotifications] = useState([]);
  const [reactNotifications, setReactNotifications] = useState([]);
  const [commentNotifications, setCommentNotifications] = useState([]);

  const [msgCounts, setMsgCounts] = useState(0);
  const [reactCounts, setReactCounts] = useState(0);
  const [commentCounts, setCommentCounts] = useState(0);

  const [msgNotificationsFetching, setMsgNotificationsFetching] =
    useState(false);
  const [reactNotificationsFetching, setReactNotificationsFetching] =
    useState(false);
  const [commentNotificationsFetching, setCommentNotificationsFetching] =
    useState(false);

  const [post, setPost] = useState("");
  const [isOpen, setIsOpen] = useState(false);

  const fetchNotificationCounts = async () => {
    const result = await axios.get(`/api/Notifications/Counts`);

    console.log(JSON.stringify(result.data));

    if (result && result.data && result.data.success) {
      setCommentCounts(result.data.data.comments);
      setReactCounts(result.data.data.reactions);
      setMsgCounts(result.data.data.messages);
    }
  };

  const fetchReactNotifications = async () => {
    setReactNotificationsFetching(true);
    const result = await axios.get(`/api/Notifications/reactions`);
    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setReactNotifications(result.data.data);
    }

    setReactNotificationsFetching(false);
  };

  const fetchMessagesNotifications = async () => {
    setMsgNotificationsFetching(true);
    const result = await axios.get(`/api/Notifications/messages`);
    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setMsgNotifications(result.data.data);
    }

    setMsgNotificationsFetching(false);
  };

  const fetchNotifications = async () => {
    // const result = await axios.get(`/api/Notifications/${account.id}`);
    // if (result && result.data && result.data.success) {
    //   console.log(result.data.data);
    //   setMsgNotifications(result.data.messages);
    //   setCommentNotifications(result.data.comments);
    //   setReactNotifications(result.data.reactNotifications);
    // }
  }

  const fetchCommentsNotifications = async () => {
    setCommentNotificationsFetching(true);
    const result = await axios.get(`/api/Notifications/comments`);
    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setCommentNotifications(result.data.data);
    }
    setCommentNotificationsFetching(false);
  };

  const fetchPost = async (id) => {
    const result = await axios.get(`/api/Posts/${id}`);

    if (result && result.data && result.data.success) {
      setPost(result.data.model);
    }
  };

  useEffect(() => {
    fetchNotificationCounts();
    fetchNotifications();
    document.addEventListener('click', () => {
      fetchNotificationCounts();
    });
    document.addEventListener('refreshNotifications', () => {
      fetchNotificationCounts();
    });
  }, [location.pathname]);

  const handleClose = () => {
    setIsOpen(false);
    fetchNotificationCounts();
  };

  return (
    <>
      <Dialog fullScreen="true" open={isOpen && post} onClose={handleClose}>
        <div className="row justify-content-start mx-4 mt-4">
          <button
            type="button"
            className="btn btn-secondary rounded-pill btn-sm mx-1"
            onClick={handleClose}
          >
            <i className="fa fa-arrow-left" aria-hidden="true"></i> Powrót
          </button>
        </div>
        <div className="row justify-content-center">
          <PostCard postData={post} setPostData={setPost} />
        </div>
      </Dialog>

      <div className="d-flex flex-row">
        <div className="mx-1 nav-item dropdown position-static justify-content-start">
          <button className="btn btn-sm btn-outline-dark"
          onMouseEnter={() => {
              if (!msgNotificationsFetching) {
                fetchMessagesNotifications();
              }
            }}
            >
            Wiadomości{" "}
            <span class="badge small badge-pill badge-danger">{msgCounts}</span>
          </button>

          {msgCounts >= 1 && (
            <div className="dropdown-content">
              <div className="text-center bg-dark text-white ">Wiadomości</div>

              {msgNotifications.map((x) => {
                return (
                  <>
                    <Link
                      to={`/chats/${x.key.id}`}
                      className="rounded-0"
                    >
                      <div><span class="badge small badge-pill badge-danger">{x.value}</span> {" "} {x.key.name}</div>
                    </Link>
                  </>
                );
              })}
            </div>
          )}
        </div>
        <div className="mx-1 nav-item dropdown position-static justify-content-start">
          <button
            className="btn btn-sm  btn-outline-dark"
            onMouseEnter={() => {
              if (
                reactCounts >= 0 &&
                reactCounts !== reactNotifications.length &&
                !reactNotificationsFetching
              ) {
                fetchReactNotifications();
              }
            }}
          >
            Reakcje{" "}
            <span class="badge small badge-pill badge-danger">
              {reactCounts}
            </span>
          </button>

          {reactCounts >= 1 && (
            <div className="dropdown-content">
              <div className="text-center bg-dark text-white ">Nowe reakcje</div>

              {reactNotifications.map((x, i) => {
                return (
                  <>
                    <div
                      className="p-1 dropdown-item"
                      onClick={() => {
                        fetchPost(x.postId);
                        setIsOpen(true);
                      }}
                    >
                      <small className="d-block">
                        {x.firstname} {x.lastname}
                      </small>
                      <label className="d-block">{x.postTitle}</label>
                    </div>
                  </>
                );
              })}
            </div>
          )}
        </div>

        {/* COMMENTS */}
        <div className="mx-1 nav-item dropdown position-static justify-content-start">
        <button
            className="btn btn-sm  btn-outline-dark"
            onMouseEnter={() => {
              if (
                commentCounts >= 0 &&
                commentCounts !== commentNotifications.length &&
                !commentNotificationsFetching
              ) {
                fetchCommentsNotifications();
              }
            }}
          >
            Komentarze{" "}
            <span class="badge small badge-pill badge-danger">
              {commentCounts}
            </span>
          </button>

          {commentCounts >= 1 && (
            <div className="dropdown-content">
              <div className="text-center bg-dark text-white ">Nowe komentarze</div>
              {commentNotifications.map((x, i) => {
                return (
                  <>
                    <div
                      className="p-1 dropdown-item"
                      onClick={() => {
                        fetchPost(x.postId);
                        setIsOpen(true);
                      }}
                    >
                      <small className="d-block">
                        {x.firstname} {x.lastname}
                      </small>
                      <label className="d-block">{x.postTitle}</label>
                    </div>
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

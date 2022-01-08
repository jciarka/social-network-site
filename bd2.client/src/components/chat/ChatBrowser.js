import React, { useState, useEffect } from "react";
import ChatEditor, { editorTypes } from "components/chat/ChatEditor";
import axios from "axios";
import ChatCard from "components/chat/ChatCard";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

const ChatBrowser = ({ type }) => {
  const [posts, setPosts] = useState([]);
  const [group, setGroup] = useState(null);
  const [canAddPosts, setCanAddPosts] = useState(true);

  const account = useSelector((state) => state.account);
  let { groupId } = useParams();

  const fetchPosts = async () => {
    let result;
    if (type === "BOARD") {
      result = await axios.get(`/api/posts/list/user/${account.id}`);
    } else {
      result = await axios.get(`/api/posts/list/group/${groupId}`);
    }

    if (result && result.data && result.data.success) {
      setPosts(
        result.data.model.map((x) => {
          return { ...x, expanded: false, detailsFetched: false };
        })
      );
    }
  };

  const fetchGroup = async () => {
    let result;
    if (type !== "BOARD") {
      setCanAddPosts(false);
      result = await axios.get(`/api/groups/${groupId}`);

      if (result && result.data && result.data.success) {
        setGroup(result.data.data);
        setCanAddPosts(result.data.canAddPosts);
      }
    }
  };

  useEffect(() => {
    fetchGroup();
    fetchPosts();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getPostDataSetter = (index) => {
    return (newData) => {
      if (newData) {
        setPosts(posts.map((x, i) => (i !== index ? x : newData)));
      } else {
        setPosts(posts.filter((x, i) => (i !== index ? true : false)));
      }
    };
  };

  return (
    <>
      <div className="container justify-content-center">
        {canAddPosts && (
          <ChatEditor
            type={editorTypes.EDIT}
            onSuccess={(x) => fetchPosts()}
            header="Stwórz nowy czat"
            post={{ groupId }}
          />
        )}

        {posts.map((postData, index) => {
          return (
            <div key={index} className="row justify-content-center">
              <ChatCard
                key={index}
                postData={postData}
                setPostData={getPostDataSetter(index)}
              />
            </div>
          );
        })}
      </div>
    </>
  );
};

export default ChatBrowser;

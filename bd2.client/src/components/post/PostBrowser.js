import React, { useState, useEffect } from "react";
import PostEditor, { editorTypes } from "components/post/PostEditor";
import axios from "axios";
import PostCard from "components/post/PostCard";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

const PostBrowser = ({ type }) => {
  const [posts, setPosts] = useState([]);

  const account = useSelector((state) => state.account);
  let { groupId } = useParams()

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

  useEffect(() => {
    fetchPosts();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getPostDataSetter = (index) => {
    return (newData) =>
      setPosts(posts.map((x, i) => (i !== index ? x : newData)));
  };

  return (
    <>
      <div className="container justify-content-center">
        <PostEditor
          type={editorTypes.EDIT}
          onSuccess={(x) => fetchPosts()}
          header="Napisz o czym myślisz"
          post={{ groupId }}
        />

        {posts.map((postData, index) => {
          return (
            <PostCard
              key={index}
              postData={postData}
              setPostData={getPostDataSetter(index)}
            />
          );
        })}
      </div>
    </>
  );
};

export default PostBrowser;

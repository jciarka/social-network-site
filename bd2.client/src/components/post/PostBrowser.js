import React, { useState, useEffect } from "react";
import PostEditor, { editorTypes } from "components/post/PostEditor";
import axios from "axios";
import PostCard from "components/post/PostCard";

const PostBrowser = ({ fetchPostsUrl, fetchPostDetailsUrl }) => {
  const [posts, setPosts] = useState([]);

  const fetchPosts = async () => {
    const result = await axios.get(fetchPostsUrl);

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
  }, []);

  const getPostDataSetter = (index) => {
    return (newData) =>
      setPosts(posts.map((x, i) => (i !== index ? x : newData)));
  };

  return (
    <>
      <div className="container justify-content-center">
        <PostEditor type={editorTypes.EDIT} header="Napisz o czym myślisz" />

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

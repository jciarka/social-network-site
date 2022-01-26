import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import PostsAbusementBrowserCard from "./PostsAbusementBrowserCard";

const PostsAbusementsBrowser = () => {
  const [abusements, setAbusements] = useState([]);
  const [posts, setPosts] = useState([]);

  const account = useSelector((state) => state.account);

  const fetchAbusements = async () => {
    let result = await axios.get(`/api/Abusements`);
    if(result) {
      setAbusements(result.data);
    }
    return result.data;
  }

  const getPosts = async (abusements) => {
    let postsArray = [];
    abusements.data.map((abusement) => {
      console.log(abusement);
      postsArray.push({"id": abusement.postId, "name": abusement.postTitle, "firstname": abusement.firstname, "lastname": abusement.lastname});
    });
    setPosts(postsArray);
    return postsArray;
  }

  useEffect(() => {
    (async () => {
      var result = await fetchAbusements();
      getPosts(result);
    })();
  }, []);

  return (
    <>
      <div className="container justify-content-center">
        {posts.map((post) => {
          return (
            <div className="row justify-content-center">
              <PostsAbusementBrowserCard
                postData={post}
              />
            </div>
          );
        })}
      </div>
    </>
  );
};

export default PostsAbusementsBrowser;

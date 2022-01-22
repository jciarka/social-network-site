import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import PostStatisticsCard from "./PostStatisticsCard";

const GroupPostStatistics = () => {
  const [posts, setPosts] = useState([]);
  let { groupId } = useParams();

  const account = useSelector((state) => state.account);

  const fetchPosts = async () => {
    let result = await axios.get(`/api/Posts/list/user/${account.id}`);
    if(result) {
      setPosts(result.data.model);
    }
  }

  useEffect(() => {
    fetchPosts();
  }, []);

  return (
    <>
      <div className="container justify-content-center">
        {posts.map((postData) => {
          return (
            <div className="row justify-content-center">
              <PostStatisticsCard 
                postData={postData}
              />
            </div>
          );
        })}
      </div>
    </>
  );
};

export default GroupPostStatistics;

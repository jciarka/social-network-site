import React, { useState, useEffect } from "react";
import axios from "axios";

const PostCard = ({ postData, setPostData }) => {
  const fetchPostsDetails = async () => {
    const result = await axios.get(`/api/Posts/${postData.post.id}`);

    if (result && result.data && result.data.success) {
      setPostData({
        ...postData,
        detailsFetched: true,
        ...result.data.model,
      });
    }
  };

  return (
    <>
      <div
        className="card m-4 p-4 pt-2 rounded rounded-lg w-100 shadow border rounded-0"
        style={{ border: "#8f8f8fb6" }}
      >
        {/* IMAGES carousel*/}
        <div className=" d-flex flex-row justify-content-between">
          <div className="h5">
            {postData.owner.firstname + " " + postData.owner.lastname}
          </div>
          <div className=" d-flex flex-row justify-content-between">
            <div className="mx-2 mt-2 h6">
              {postData.post.postDate.slice(0, 19).split("T").join("  ")}
              {/* TO DO: cut milieconds */}
            </div>

            <div className="mx-2 btn btn-sm btn-primary">
              <i className="fa fa-thumbs-up mr-2" aria-hidden="true"></i>
              {postData.post.positiveReactionsCount}
            </div>
            <div className="mx-2 btn btn-sm btn-danger">
              <i className="fa fa-thumbs-down mr-2" aria-hidden="true"></i>
              {postData.post.negativeReactionCount}
            </div>
            <div className="mx-2 btn btn-sm btn-warning">
              <i className="fa fa-comment mr-2" aria-hidden="true"></i>
              {postData.post.commentsCount}
            </div>
          </div>
        </div>
        <div className="h5 justify-content-center text-center">
          {postData.post.title}
        </div>
        <div>{postData.post.text}</div>
      </div>
    </>
  );
};

export default PostCard;

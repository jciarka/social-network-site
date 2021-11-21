import React from "react";
import { useState } from "react";
import CommentBubble from "./CommentBubble";
import axios from "axios";
import { useSelector } from "react-redux";

const PostComments = ({
  postId,
  comments,
  setComments,
  showComments = false,
}) => {
  const account = useSelector((state) => state.account); // get redux store values
  const [newComment, setNewComment] = useState({
    text: "",
  });

  const postComment = async () => {
    console.log(`posting ${newComment}`);
    const result = await axios.post(
      `/api/PostComments/${postId}/${account.id}`, { text: newComment.text}
    );

    if (result && result.data && result.data.success) {
      setComments([...comments, result.data]);
    }
  };

  return (
    <div className="row justify-content-center m-0 p-0 mb-4">
      <div className="col-lg-10 p-2 m-0">
        <form
          onSubmit={(e) => {
            e.preventDefault();
            postComment();
          }}
        >
          <div className="text-center">
            <span className="card-title">Napisz co o tym myślisz?</span>
          </div>

          <textarea
            className="w-100"
            style={{ height: "150px", resize: "none" }}
            placeholder={
              account.isLoggedIn === false ? "Log in to post a comment" : ""
            }
            value={newComment.text}
            onChange={(e) =>
              setNewComment({ ...newComment, text: e.target.value })
            }
          ></textarea>

          <button
            type="submit"
            className="rounded-0 btn btn-primary btn-lg w-100 m-auto"
            disabled={account.isLoggedIn === false}
          >
            Wyślij
          </button>
        </form>
      </div>

      {!comments ||
        (showComments && comments.length !== 0 && (
          <div className="w-100">

            {comments.map((comment, index) => (
              <CommentBubble key={index} comment={comment} />
            ))}
          </div>
        ))}
    </div>
  );
};

export default PostComments;

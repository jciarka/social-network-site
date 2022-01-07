import ImageGallery from "react-image-gallery";
import "react-image-gallery/styles/css/image-gallery.css";
import axios from "axios";
import PostComments from "./PostComments";
import { useSelector } from "react-redux";
import Dialog from "@mui/material/Dialog";
import { useState } from "react";
import AddAbusement from "components/abusements/AddAbusement";
import DeletePost from "components/post/DeletePost";

const PostCard = ({ postData, setPostData }) => {
  const account = useSelector((state) => state.account); // get redux store values
  const [open, setOpen] = useState(false);
  const [abusement, setAbusement] = useState({});
  const [deletePostOpen, setDeletePostOpen] = useState(false);
  const [deletedPostId, setDeletdPostId] = useState({});

  const fetchPostsDetails = async () => {
    const result = await axios.get(`/api/Posts/${postData.post.id}`);

    if (result && result.data && result.data.success) {
      setPostData({
        ...postData,
        detailsFetched: true,
        expanded: true,
        ...result.data.model,
      });
    }
  };
  const postReaction = async (type) => {
    const result = await axios.post(
      `/api/PostReaction/${postData.post.id}/${account.id}`,
      { type }
    );

    if (result && result.data && result.data.success) {
      postData.hasReacted = type;
      setPostData(postData);
    }
  };

  const getImages = () => {
    return postData.images.map((x) => {
      return {
        original: "/Images/" + x,
        thumbnail: "/Images/" + x,
      };
    });
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleDeletePostClickOpen = () => {
    setDeletePostOpen(true);
  };

  const handleDeletePostClose = () => {
    setDeletePostOpen(false);
  };

  return (
    <>
      <Dialog open={open} onClose={handleClose}>
        <AddAbusement
          abusement={abusement}
          setAbusement={setAbusement}
          onCancel={handleClose}
          onSuccess={() => {
            setPostData({ ...postData, hasNotifiedAbusement: true });
            handleClose();
          }}
        />
      </Dialog>

      <Dialog open={deletePostOpen} onClose={handleDeletePostClose}>
        <DeletePost
          postId={deletedPostId}
          onCancel={handleDeletePostClose}
          onSuccess={() => {
            setPostData(null);
            handleClose();
          }}
        />
      </Dialog>

      <div
        className="card m-4 pt-0 pb-0 rounded rounded-lg w-100 shadow border border-dark rounded-0"
        style={{ border: "#8f8f8fb6", maxWidth: "800px" }}
      >
        {postData.isOwner && (
          <div className="row w-100 justify-content-end m-0 p-0">
            <button
              type="button"
              className="btn btn-outline-danger rounded-circle btn-sm mt-1 mr-1"
              onClick={() => {
                setDeletdPostId(postData.post.id)
                handleDeletePostClickOpen()
              }}
            >
              <i class="fa fa-times" aria-hidden="true"></i>
            </button>
          </div>
        )}

        {!postData.isOwner && (
          <div className="mt-3" />
        )}
        
        <div className="mx-4 mt-1">
          {postData.images && postData.images.length > 0 && (
            <ImageGallery
              items={getImages()}
              showBullets={true}
              showIndex={true}
              showThumbnails={false}
            />
          )}
        </div>
        <div style={{ marginBottom: 10 }} />

        {/* IMAGES carousel*/}
        <div className="mx-4 d-flex flex-row justify-content-between">
          <div className="h5">
            {postData.owner.firstname + " " + postData.owner.lastname}
          </div>
          <div className="my-0 py-0 d-flex flex-row justify-content-between">
            <div className="mx-2 mt-1 h6">
              {postData.post.postDate.slice(0, 19).split("T").join("  ")}
              {/* TO DO: cut milieconds */}
            </div>

            <button
              className={`mx-2 badge badge-pill py-2 shadow-lg ${
                postData.hasReacted === 2 ? "badge-primary" : ""
              }`}
              style={{
                height: 30,
                fontSize: 13,
              }}
              onClick={() => {
                postReaction(2);
              }}
            >
              <i className="fa fa-thumbs-up mr-2" aria-hidden="true"></i>
              {postData.post.positiveReactionsCount}
            </button>
            <button
              className={`mx-2 badge badge-pill py-2 shadow-lg ${
                postData.hasReacted === 1 ? "badge-danger" : ""
              }`}
              style={{
                height: 30,
                fontSize: 13,
              }}
              onClick={() => {
                postReaction(1);
              }}
            >
              <i className="fa fa-thumbs-down mr-2" aria-hidden="true"></i>
              {postData.post.negativeReactionCount}
            </button>
            <div
              className="mx-2 badge badge-pill badge-warning py-2"
              style={{ height: 30, fontSize: 13 }}
            >
              <i className="fa fa-comment mr-2" aria-hidden="true"></i>
              {postData.post.commentsCount}
            </div>
            {postData.hasNotifiedAbusement && (
              <div
                className="mx-2 badge badge-pill border border-dark badge-danger py-2"
                style={{ height: 30, fontSize: 13 }}
              >
                <i
                  class="fa fa-exclamation-triangle text-white"
                  aria-hidden="true"
                ></i>
              </div>
            )}
            {!postData.hasNotifiedAbusement && (
              <div
                className="mx-2 badge badge-pill border border-secnodary badge-black py-2"
                style={{ height: 30, fontSize: 13 }}
                onClick={() => {
                  setAbusement({
                    postId: postData.post.id,
                    text: "",
                  });
                  handleClickOpen();
                }}
              >
                <i
                  class="fa fa-exclamation-triangle text-secondary"
                  aria-hidden="true"
                ></i>
              </div>
            )}
          </div>
        </div>
        <hr style={{ background: "black", marginBottom: 10, marginTop: 10 }} />
        <div className="h5  mx-4  justify-content-center text-center">
          {postData.post.title}
        </div>
        <div className="ma-4 pa-4  mx-4 " style={{ fontSize: 18 }}>
          {postData.post.text}
        </div>

        <div className="mt-2 mx-4 mb-0 pb-0">
          <PostComments
            comments={postData.comments}
            setComments={(comments) => {
              setPostData({ ...postData, comments });
            }}
            showComments={postData.expanded}
            postId={postData.post.id}
          />
        </div>
        {!postData.expanded ? (
          <div
            className="badge rounded-0 badge-secondary py-2 text-right"
            style={{
              height: 17,
              fontSize: 13,
              position: "relative",
              top: "1px",
            }}
            onClick={() => {
              fetchPostsDetails();
              setPostData({ ...postData, expanded: true });
            }}
          >
            <i
              style={{ position: "relative", top: "-7px" }}
              className="fa fa-arrow-down mr-2"
              aria-hidden="true"
            ></i>
            <span style={{ position: "relative", top: "-7px" }}>
              Pokaż komentarze
            </span>
          </div>
        ) : (
          <div
            className="badge rounded-0 badge-secondary py-2 text-left"
            style={{
              height: 17,
              fontSize: 13,
              position: "relative",
              top: "1px",
            }}
            onClick={() => {
              setPostData({ ...postData, expanded: false });
            }}
          >
            <i
              style={{ position: "relative", top: "-7px" }}
              className="fa fa-arrow-up mr-2"
              aria-hidden="true"
            ></i>
            <span style={{ position: "relative", top: "-7px" }}>
              Ukryj komentarze
            </span>
          </div>
        )}
      </div>
    </>
  );
};

export default PostCard;

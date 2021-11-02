import React, { useState } from "react";
import ImagePicker from "../general/ImagePicker";
import axios from "axios";

export const editorTypes = {
  EDIT: "EDIT",
  NEW: "NEW",
};

const PostEditor = ({
  post = null,
  onSuccess = null,
  type = editorTypes.NEW,
  header,
}) => {
  const [title, setTitle] = useState(post ? post.title : "");
  const validateTitle = () => {
    if (title.length === 0 || title === "") return "Tytuł nie może być puste";
    else return "";
  };

  const [text, setText] = useState(post ? post.text : "");
  const validateText = () => {
    if (text.length === 0 || text === "") return "Opis nie może być pusty";
    else return "";
  };

  const [backendErrors, setBackendErrors] = useState([]);

  const validateAll = () => {
    if (validateTitle() !== "") return false;
    if (validateText() !== "") return false;
    return true;
  };

  const [files, setFiles] = useState([]);

  const submit = async () => {
    try {
      const result = await axios.post("api/posts", {
        title: title,
        text: text,
      });

      if (
        result &&
        result.data &&
        result.data.success &&
        result.data.model.post.id
      ) {
        setTitle("");
        setText("");
        setFiles([]);
      } else {
        setBackendErrors(result.data.errors);
        return;
      }

      for (let index = 0; index < files.length; index++) {
        const form = new FormData();
        form.append("image", files[index]);
        const imageResponse = await axios.post(
          `api/posts/images/${result.data.model.post.id}`,
          form
        );

        if (imageResponse && imageResponse.data && imageResponse.data.success) {
          if (!result.data.model.images) {
            result.data.model.images = [];
          }
          result.data.model.images.push(imageResponse.data.model);
        } else {
          setBackendErrors(result.data.errors);
          return;
        }
      }
      if (onSuccess) {
        onSuccess(result.data.model);
      }
    } catch (e) {
      if (e.response) {
        setBackendErrors([e.message]);
      } else {
        setBackendErrors(e);
      }
    }
  };

  return (
    <div
      className="container d-flex justify-content-center"
      style={{"max-width": 600}}
    >
      <div
        className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
        style={{ border: "#8f8f8fb6" }}
      >
        {header && (
          <div className=" text-center">
            <h4>{header}</h4>
          </div>
        )}
        <div className="form-group m-0">
          <label htmlFor="title">Tytuł</label>
          <input
            type="text"
            className="form-control"
            placeholder="Tytuł"
            value={title}
            style={validateTitle() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setTitle(e.target.value);
              validateTitle();
            }}
          />
          <label className="text-danger text-sm-left">{validateTitle()}</label>
        </div>

        <div className="form-group m-0">
          <label htmlFor="text">Opis</label>
          <textarea
            type="text"
            className="form-control"
            placeholder="Opis"
            value={text}
            style={validateText() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setText(e.target.value);
            }}
          />
          <label className="text-danger text-sm-left">{validateText()}</label>
        </div>

        <div className="container justify-content-center">
          <ImagePicker
            files={files}
            setFiles={setFiles}
            className="m-3"
            style={{ height: "50px" }}
          />
        </div>

        {backendErrors && backendErrors.length > 0 && (
          <div className="pb-2 alert alert-danger p-0 d-flex align-items-center">
            <div className="text-center w-100" role="alert">
              {backendErrors.map((error, index) => {
                return (
                  <div key={index}>
                    <label>{error}</label>
                  </div>
                );
              })}
            </div>
            <div
              className="close small m-1 p-1 align-middle"
              data-dismiss="alert"
              style={{ fontSize: "20px", display: "block" }}
            >
              <span
                style={{ display: "block" }}
                onClick={(e) => {
                  e.preventDefault();
                  setBackendErrors([]);
                }}
              >
                &times;
              </span>
            </div>
          </div>
        )}

        <div className="mt-2 w-100 text-center">
          <button
            disabled={!validateAll()}
            type="submit"
            className="btn btn-primary rounded-0"
            onClick={(e) => submit()}
          >
            Utwórz
          </button>
        </div>
      </div>
    </div>
  );
};

export default PostEditor;

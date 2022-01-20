import React, { useState } from "react";
import axios from "axios";
import { Autocomplete, TextField } from "@mui/material";

export const editorTypes = {
  EDIT: "EDIT",
  NEW: "NEW",
};

const ChatEditor = ({
  chat = null,
  onSuccess = null,
  type = editorTypes.NEW,
  header,
}) => {
  const [name, setName] = useState(chat ? chat.name : "");
  const validateName = () => {
    if ((name && name.length === 0) || name === "")
      return "Nazwa nie może być pusta";
    else return "";
  };

  const [member, setMember] = useState(chat ? chat.member : "");
  const validateMember = () => {
    if ((member && member.length === 0) || member === "")
      return "Członek nie może być pusty";
    else return "";
  };

  const [backendErrors, setBackendErrors] = useState([]);

  const validateAll = () => {
    if (validateName() !== "") return false;
    if (validateMember() !== "") return false;
    return true;
  };

  const [open, setOpen] = React.useState(false);
  const [people, setPeople] = React.useState([]);

  const fetchPeopleByName = async (phrase) => {
    const result = await axios.get(`/api/accounts/findByName/${phrase}`);

    if (result && result.data && result.data.success) {
      setPeople(result.data.data);
    }
  };


  const submit = async () => {
    try {
      const result = await axios.post("/api/chat", {
        ...chat,
        name: name,
        memberId: member.id,
      });

      if (
        result &&
        result.data &&
        result.data.success) 
        {
        setName("");
        setMember("");
      } else {
        setBackendErrors(result.data.errors);
        return;
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
      style={{ "max-width": 600 }}
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
          <label htmlFor="name">Nazwa</label>
          <input
            type="text"
            className="form-control"
            placeholder="Nazwa"
            value={name}
            onChange={(e) => {
              setName(e.target.value);
              validateName();
            }}
          />
        </div>

        <div className="form-group m-0">
          <label htmlFor="member">Członek</label>    
            <Autocomplete
              fullWidth
              noOptionsText="Wpisz frazę"
              size="small"
              open={open}
              filter
              onOpen={() => {
                setOpen(true);
              }}
              onClose={() => {
                setOpen(false);
              }}
              isOptionEqualToValue={(option, value) => option.id === value.id}
              getOptionLabel={(option) =>
                option.firstname + " " + option.lastname
              }
              options={people}
              renderInput={(params) => (
                <TextField
                  type="member"
                  {...params}
                  label=""
                  InputProps={{
                    ...params.InputProps,
                  }}
                  onInput={(e) => {
                    if (e.target.value && e.target.value.length > 0)
                      fetchPeopleByName(e.target.value);
                  }}
                />
              )}
              onChange={(event, newInputValue) => {
                setMember(newInputValue);
              }}
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

export default ChatEditor;

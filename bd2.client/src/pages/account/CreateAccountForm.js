import React, { useState } from "react";
import { useHistory } from "react-router-dom";

const CreateAccountForm = () => {
  const history = useHistory();

  const [firstname, setFirstname] = useState("");
  const validateFirstname = () => {
    if (firstname.length === 0 || firstname === "")
      return "Imię nie może być puste";
    else return "";
  };

  const [lastname, setLastname] = useState("");
  const validateLastname = () => {
    if (lastname.length === 0 || lastname === "")
      return "Imię nie może być puste";
    else return "";
  };

  const [password, setPassword] = useState("");
  const validatePassword = () => {
    if (password.length === 0 || password === "")
      return "Password can't be empty";
    else return "";
  };

  const [retypedPassword, setRetypedPassword] = useState("");
  const validateRetypedPassword = () => {
    if (retypedPassword !== password) return "Passwords are not equal";
    else return "";
  };

  const [email, setEmail] = useState("");
  const validateEmail = () => {
    var pattern = new RegExp(
      /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i
    );
    if (!pattern.test(email)) return "Email is not valid";
    else return "";
  };

  const [backendErrors, setBackendErrors] = useState([]);

  const validateAll = () => {
    if (validateFirstname() !== "") return false;
    if (validateLastname() !== "") return false;
    if (validatePassword() !== "") return false;
    if (validateRetypedPassword() !== "") return false;
    if (validateEmail() !== "") return false;
    return true;
  };

  const putNewUser = async () => {
    await fetch("/api/auth/register", {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify({
        firstname: firstname,
        lastname: lastname,
        password: password,
        email: email,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          // kod inny niż 200 (błedy HTTP)
          response
            .json()
            .then((data) => setBackendErrors(data.errors));
        } else {
          response
            .json()
            .then((data) => {
              // kod 200 
              if (data.success) {
                history.push("/login");
              } else {
                console.log(data.errors);
                setBackendErrors(data.errors);
              }
            })
        }
      })
      
  };

  return (
    <div className="container d-flex justify-content-center">
      <div
        className="card m-4 rounded rounded-lg w-100 shadow border rounded-0"
        style={{ border: "#8f8f8fb6", maxWidth: 500, padding: 60 }}
      >
        <div className="form-group m-0">
          <label htmlFor="username">E-mail</label>
          <input
            type="text"
            className="form-control"
            placeholder="E-mail"
            value={email}
            style={validateEmail() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setEmail(e.target.value);
              validateEmail();
            }}
          />
          <label className="text-danger text-sm-left">{validateEmail()}</label>
        </div>

        <div className="form-group m-0">
          <label htmlFor="firstname">Imię</label>
          <input
            type="text"
            className="form-control"
            placeholder="Imię"
            value={firstname}
            style={validateFirstname() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setFirstname(e.target.value);
            }}
          />
          <label className="text-danger text-sm-left">
            {validateFirstname()}
          </label>
        </div>

        <div className="form-group m-0">
          <label htmlFor="lastname">Nazwisko</label>
          <input
            type="text"
            className="form-control"
            placeholder="Nazwisko"
            value={lastname}
            style={validateLastname() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setLastname(e.target.value);
            }}
          />
          <label className="text-danger text-sm-left">
            {validateLastname()}
          </label>
        </div>

        <div className="form-group m-0">
          <label htmlFor="password">Hasło</label>
          <input
            type="password"
            className="form-control"
            placeholder="Hasło"
            value={password}
            style={validatePassword() !== "" ? { borderColor: "red" } : {}}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          />
          <label className="text-danger text-sm-left">
            {validatePassword()}
          </label>
        </div>

        <div className="form-group  m-0">
          <label htmlFor="password">Powtórz hasło</label>
          <input
            type="password"
            className="form-control"
            placeholder="Powtórz hasło"
            value={retypedPassword}
            style={
              validateRetypedPassword() !== "" ? { borderColor: "red" } : {}
            }
            onChange={(e) => {
              setRetypedPassword(e.target.value);
            }}
          />
          <label className="text-danger text-sm-left">
            {validateRetypedPassword()}
          </label>
        </div>

        {backendErrors && backendErrors.length > 0 && (
          <div className="p-2 alert alert-danger p-0 pt-1 d-flex align-items-center">
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

        <div className="w-100 text-center">
          <button
            disabled={!validateAll()}
            type="submit"
            className="btn btn-primary rounded-0"
            onClick={(e) => putNewUser()}
          >
            Utwórz
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateAccountForm;

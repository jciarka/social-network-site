import React, { useState } from "react";
import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { useDispatch, useSelector } from "react-redux";
import { actionCreators } from "../../store/index";

const LoginForm = () => {
  const [emial, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const account = useSelector((state) => state.account);
  const dispach = useDispatch();
  const { login, clearErrors } = bindActionCreators(actionCreators, dispach);

  return (
    <div className="container d-flex justify-content-center">
      <div
        className="card m-4 rounded rounded-0 w-100 shadow border "
        style={{ border: "#8f8f8fb6", maxWidth: 500 }}
      >
        <div style={{ padding: 60 }}>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              login(emial, password);
            }}
          >
            <div className="form-group">
              <label htmlFor="username">E-mail</label>
              <input
                type="text"
                className="form-control"
                id="username"
                aria-describedby="E-mail"
                placeholder="E-mail"
                value={emial}
                onChange={(e) => {
                  setEmail(e.target.value);
                }}
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">Hasło</label>
              <input
                type="password"
                className="form-control"
                id="hasło"
                placeholder="Hasło"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value);
                }}
              />
            </div>

            {account.errors && account.errors.length > 0 && (
              <div className="p-2 alert alert-danger p-0 pt-1 d-flex align-items-center">
                <div className="text-center w-100" role="alert">
                  {account.errors.map((error, index) => {
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
                      clearErrors();
                    }}
                  >
                    &times;
                  </span>
                </div>
              </div>
            )}

            <div className="w-100 text-center">
              <button type="submit" className="btn btn-primary rounded-0">
                Zaloguj się
              </button>
            </div>

            <div className="w-100 mt-4 text-center ">
              Nie masz jeszcze konta? <br />
              <Link to="/createAccount" className="btn btn-dark rounded-0">
                Zarejestruj się!
              </Link>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default LoginForm;

import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import UsersAbusementBrowserCard from "./UsersAbusementBrowserCard";

const UsersAbusementsBrowser = () => {
  const [abusements, setAbusements] = useState([]);
  const [users, setUsers] = useState([]);

  const account = useSelector((state) => state.account);

  const fetchAbusements = async () => {
    let result = await axios.get(`/api/Abusements`);
    if(result) {
      setAbusements(result.data);
    }
    return result.data;
  }

  const getUsers = async (abusements) => {
    let usersArray = [];
    abusements.data.map((abusement) => {
      usersArray.push({"id": abusement.accountId, "firstname": abusement.firstname, "lastname": abusement.lastname});
    });
    setUsers(usersArray);
    return usersArray;
  }

  useEffect(() => {
    (async () => {
      var result = await fetchAbusements();
      getUsers(result);
    })();
  }, []);

  return (
    <>
      <div className="container justify-content-center">
        {users.map((user) => {
          return (
            <div className="row justify-content-center">
              <UsersAbusementBrowserCard
                userData={user}
              />
            </div>
          );
        })}
      </div>
    </>
  );
};

export default UsersAbusementsBrowser;

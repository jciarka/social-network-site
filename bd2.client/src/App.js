import React from "react";
import Header from "components/Header.js";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { useSelector } from "react-redux";
import { Redirect } from "react-router-dom";

import LoginForm from "pages/account/LoginForm";
import CreateAccountForm from "pages/account/CreateAccountForm";
import PostBrowser from "components/post/PostBrowser";
import GroupBrowser from "pages/groups/GroupBrowser";
import AdminGroupBrowser from "pages/groups/administration/AdminGroupBrowser";
import PacketSubcriptionsBrowser from "pages/subscriptions/PacketSubcriptionsBrowser";
import AdminGroupUsersBrowser from "pages/groups/administration/AdminGroupUsersBrowser";
import AbusementsManager from 'pages/abusements/AbusementsManager'
const App = () => {
  // const serverUrl = "http://localhost:8080/"
  const account = useSelector((state) => state.account); // get redux store values

  return (
    <Router>
      <Header />

      <Route path="/login" exact render={(props) => <LoginForm />} />

      <Route
        path="/createAccount"
        exact
        render={(props) => (
          <>
            <CreateAccountForm />
          </>
        )}
      />

      <Route
        path="/board"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <PostBrowser type={"BOARD"} />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      />

      <Route
        path="/group/:groupId"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <PostBrowser type={"GROUP"} />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      />

      <Route
        path="/groups"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <GroupBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        exact
        path="/groups/administration"
        render={(props) => (
          <>
            {account && account.isLoggedIn && <AdminGroupBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        exact
        path="/groups/administration/:groupId/users"
        render={(props) => (
          <>
            {account && account.isLoggedIn && <AdminGroupUsersBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/subscriptions"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <PacketSubcriptionsBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/abusements"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <AbusementsManager />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("MODERATOR")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/abusements/stats"
        exact
        render={(props) => (
          <>
            TO DO: abusements stats
            {/* {account && account.isLoggedIn && <PacketSubcriptionsBrowser />} */}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("MODERATOR")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/packetsmanager"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <PacketSubcriptionsBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/packetsmanager/stats"
        exact
        render={(props) => (
          <>
            TO DO: packets stats
            {/* {account && account.isLoggedIn && <PacketSubcriptionsBrowser />} */}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("ADMIN")) && <Redirect to="/login" />}
          </>
        )}
      ></Route>

      <Route
        path="/chats"
        exact
        render={(props) => (
          <>
            {<ChatBrowser />}
            {/* {account && account.isLoggedIn && <ChatBrowser />}
            {(!account ||
              !account.isLoggedIn ||
              !account.roles ||
              !account.roles.includes("USER")) && <Redirect to="/login" />} */}
          </>
        )}
      ></Route>
      
    </Router>
  );
};

// npm i redux
// npm i react-redux
// npm i redux-thunk
// npm install axios

// npm install react-image-gallery
// npm install @mui/material @emotion/react @emotion/styled

export default App;

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
            {(!account || !account.isLoggedIn) && <Redirect to="/login" />}
          </>
        )}
      />

      <Route
        path="/group/:groupId"
        exact
        render={(props) => (
          <>
            {account && account.isLoggedIn && <PostBrowser type={"GROUP"} />}
            {(!account || !account.isLoggedIn) && <Redirect to="/login" />}
          </>
        )}
      />

      <Route
        path="/groups"
        exact
        render={(props) => (
          <>
            <GroupBrowser />
          </>
        )}
      ></Route>

      <Route
        exact
        path="/groups/administration"
        render={(props) => (
          <>
            <AdminGroupBrowser />
          </>
        )}
      ></Route>

      <Route
        exact
        path="/groups/administration/:groupId/users"
        render={(props) => (
          <>
            <AdminGroupUsersBrowser />
          </>
        )}
      ></Route>

      <Route
        path="/subscriptions"
        render={(props) => (
          <>
            <PacketSubcriptionsBrowser />
          </>
        )}
      ></Route>

      {/* <Route path="/" exact render={(props) => <Browser />} />

      <Route path="/login" exact render={(props) => <LoginForm />} />

      <Route path="/hotels" exact render={(props) => <HotelManager />} />

      <Route
        path="/reservations"
        exact
        render={(props) => (
          <>
            <ReservationsManager />
          </>
        )}
      />
    */}
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

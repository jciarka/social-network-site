import Header from "./components/Header.js";
import { BrowserRouter as Router, Route } from "react-router-dom";
import LoginForm from "./pages/account/LoginForm";
import CreateAccountForm from "./pages/account/CreateAccountForm";
import ImagePicker from "./components/general/ImagePicker";

import { useState } from "react";

const App = () => {
  // const serverUrl = "http://localhost:8080/"

  const [files, setFiles] = useState([]);

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

      <div className="container justify-content-center">
        <ImagePicker files={files} setFiles={setFiles} className="m-3" style={{ height: "100px" }} />
      </div>

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

export default App;

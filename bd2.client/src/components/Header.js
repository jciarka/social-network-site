import { Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { useDispatch, useSelector } from "react-redux";
import LogoutIcon from "../icons/exit.svg";
import { actionCreators } from "../store/index";

const Header = () => {
  const account = useSelector((state) => state.account); // get redux store values
  const dispach = useDispatch();
  const { logout } = bindActionCreators(actionCreators, dispach);

  return (
    <>
      {/*<!-- NAVBAR -->*/}
      <nav
        className="navbar navbar-expand-lg navbar-light shadow shadow-sm border-bottom border-dark"
        style={{ border: "#8f8f8fb6" }}
      >
        {/*<!-- Links -->*/}
        <div className="navbar-brand text-center m-0 p-0">
          <div className=" m-0 p-0">
            <div className="prompt">
              {" "}
              <strong>I don't have a title yet!</strong>
            </div>
            <div className="d-flex justify-content-start m-0 p-0">
              <Link to="/board">
                <button
                  className="btn btn-dark nav-button rounded-0"
                  style={{ position: "relative", top: "9px" }}
                >
                  {/* <img src={RestaurantLogo} width="20" height="20" alt=""/> */}
                  Tablica
                </button>
              </Link>
              <Link to="/groups">
                <button
                  className="btn btn-dark nav-button rounded-0"
                  style={{ position: "relative", top: "9px" }}
                >
                  {/* <img src={HistoryLogo} width="20" height="20" alt=""/> */}
                  Grupy
                </button>
              </Link>
              <Link to="/">
                <button
                  className="btn btn-dark nav-button rounded-0"
                  style={{ position: "relative", top: "9px" }}
                >
                  {/* <img src={PopcornLogo} width="20" height="20" alt=""/> */}
                  Znajomi
                </button>
              </Link>
              <Link to="/">
                <button
                  className="btn btn-dark nav-button rounded-0"
                  style={{ position: "relative", top: "9px" }}
                >
                  {/* <img src={FootballLogo} width="20" height="20" alt=""/> */}
                  Komunikator
                </button>
              </Link>
            </div>
          </div>
        </div>

        {/*<!-- LOGIN -->*/}
        <div
          className="collapse navbar-collapse justify-content-end"
          id="navbarSupportedContent"
        >
          <ul className="navbar-nav">
            <li className="nav-item active">
              {/* { authInfo.isLoggedIn ?  */}

              {account && account.isLoggedIn && (
                <div className="nav-item dropdown position-static justify-content-end">
                  <button
                    className="btn btn-light rounded-circle btn-primary p-0"
                    style={{ display: "block", width: "40px", height: "40px" }}
                  >
                    <i
                      className="fa fa-user-circle-o fa-lg"
                      aria-hidden="true"
                    ></i>
                  </button>
                  <div className="dropdown-content">
                    <div className="text-center bg-dark text-white ">
                      {" "}
                      {account && account.firstname}{" "}
                      {account && account.lastname}{" "}
                    </div>
                    <Link
                      to="/"
                      className="rounded-0"
                      onClick={(e) => {
                        e.preventDefault();
                        logout();
                      }}
                    >
                      Log out{" "}
                      <img
                        className="bl-2"
                        src={LogoutIcon}
                        width="15"
                        height="15"
                        alt=""
                      />
                    </Link>
                    <Link to="/groups/administration" className="rounded-0">
                      Moje grupy <i class="fa fa-users" aria-hidden="true"></i>
                    </Link>
                    <Link to="/subscriptions" className="rounded-0">
                      Subskrypcje{" "}
                      <i class="fa fa-ticket" aria-hidden="true"></i>
                    </Link>
                  </div>
                </div>
              )}

              {!account ||
                (!account.isLoggedIn && (
                  <div className="nav-item dropdown position-static justify-content-end">
                    <Link to="/login">
                      <button
                        className="btn btn-light rounded-circle btn-primary p-0"
                        style={{
                          display: "block",
                          width: "40px",
                          height: "40px",
                        }}
                      >
                        <i
                          className="fa fa-sign-in fa-lg"
                          aria-hidden="true"
                        ></i>
                      </button>
                    </Link>
                  </div>
                ))}
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
};

export default Header;

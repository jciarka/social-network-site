import { createStore, applyMiddleware } from "redux";
import reducers from "./reducers/index";
import thunk from "redux-thunk";

export const store = createStore(
  reducers, // combined reducers actions
  {}, // default state (empty)
  applyMiddleware(thunk)
);

const initAccountState = () => {
  return {
    isLoggedIn: false,
    id: null,
    email: null,
    firstname: null,
    lastname: null,
    roles: [],
    token: null,
    errors: [],
  };
};

const accountReducer = (state = initAccountState(), action) => {
  switch (action.type) {
    case "SET_LOG_IN":
      return {
        isLoggedIn: true,
        ...action.payload,
        errors: [],
      };
    case "LOG_OUT":
      return initAccountState();
    case "SET_ERRORS":
      return {
        ...initAccountState(),
        errors: action.payload.errors,
      };
    case "CLEAR_ERRORS":
      return {
        ...state,
        errors: [],
      };
    default:
      return state;
  }
};

export default accountReducer;

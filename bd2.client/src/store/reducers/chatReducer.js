const initChatState = () => {
    return {
      isUpToDate: true,
      id: null,
    };
  };
  
  const chatReducer = (state = initChatState(), action) => {
    switch (action.type) {
      case "SET_REFRESH":
        return {
          ...state,
          isUpToDate: false
      };
      case "SET_UPTODATE":
        return {
          ...state,
          isUpToDate: true
      };
      case "SET_INIT":
        return {
          ...action.payload,
      };
      default:
        return state;
    }
  };
  
  export default chatReducer;
  
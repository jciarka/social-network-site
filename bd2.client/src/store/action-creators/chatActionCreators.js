export const initChat = (chatId) => {
  return (dispatch) => {
    dispatch({
      type: "SET_INIT",
      payload: {
        id: chatId,
        isUpToDate: false
        },
    });
  };
};

export const setRefreshChat = () => {
  return (dispatch) => {
    dispatch({
      type: "SET_REFRESH",
    });
  };
};

export const setUpToDateChat = () => {
    return (dispatch) => {
      dispatch({
        type: "SET_UPTODATE",
      });
    };
  };

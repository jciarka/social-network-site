const exampleReducerInit = () => {
  return {
    someData: 0,
  };
};

const exampleReducer = (state = exampleReducerInit(), action) => {
  switch (action.type) {
    case "ACTION":
      return {
        ...state,
        someData: state.someData + 1,
      };
    default:
      return state;
  }
};

export default exampleReducer
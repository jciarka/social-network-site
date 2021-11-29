import React, { useEffect } from "react";
// import axios from "axios";

const GroupBrowser = ({abc, def}) => {

  const fetchGroups = async () => {
    // const result = await axios.get();

    // if (result && result.data && result.data.success) {
    //   setPosts(
    //     result.data.model.map((x) => {
    //       return { ...x, expanded: false, detailsFetched: false };
    //     })
    //   );
    // }
  };

  useEffect(() => {
    fetchGroups();
  }, []);

  return (
    <>
      <div className="container justify-content-center">
        absc
      </div>
    </>
  );
};

export default GroupBrowser;

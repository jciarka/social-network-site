import React, { useState, useEffect } from "react";
import axios from "axios";


const AdminGroupBrowser = () => {
  const [groups, setGroups] = useState([]);

  const fetchGroups = async () => {
    const result = await axios.get("/api/Groups/find", {
      IsMember: true,
    });

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      groups(result.data.data);
    }
  };

  useEffect(() => {
    fetchGroups();
  }, []);

  return (
    <>
      <div className="container justify-content-center">absc</div>
    </>
  );
};

export default AdminGroupBrowser;

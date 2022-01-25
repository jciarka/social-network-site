import React, { useState, useEffect, useRef } from "react";
import axios from "axios";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import GroupStatisticsCard from "./GroupStatisticsCard";

const ViewsStatisticsBrowser = () => {
  const [groups, setGroups] = useState([]);

  const account = useSelector((state) => state.account);

  const fetchGroups = async () => {
    let result = await axios.get(``);
    if(result && result.success) {
      setGroups(result.data);
    }
  }

  useEffect(() => {
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <div className="container justify-content-center">
        {groups.map((groupData) => {
          return (
            <div className="row justify-content-center">
              <GroupStatisticsCard
                groupData={groupData}
              />
            </div>
          );
        })}
        <div className="row justify-content-center">
          <GroupStatisticsCard
            groupData={{name: "Posty dla moderatora", id: account.id}}
          />
        </div>
      </div>
    </>
  );
};

export default ViewsStatisticsBrowser;

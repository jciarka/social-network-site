import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link, useParams } from "react-router-dom";
import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid } from 'recharts';

const PostStatisticsCard = (postData) => {
  const post = postData.postData;
  const [postViewsWeek, setPostViewsWeek] = useState([]);
  const [chartData, setChartData] = useState([]);

  // useEffect(() => {
  //   getLastWeek();
  // }, []);

  useEffect(() => {
    (async () => {
      var result = await getLastWeek();
      generateChartDataForWeek(result);
    })();
  }, []);

  const getLastWeek = async () => {
    var result = await axios.get(`/api/Posts/list/views/${post.post.id}`);
    var postViews = result.data.model;
    var postViewsWeekArray = [];
    var lastWeekDate = new Date();
    lastWeekDate.setDate(lastWeekDate.getDate() - 7);
    postViews.map((postView) => {
      var viewDate = new Date(postView.viewDate);
      if(viewDate > lastWeekDate)
        postViewsWeekArray.push(viewDate);
    });
    setPostViewsWeek(postViewsWeekArray);
    return postViewsWeekArray;
  };

  const generateChartDataForWeek = async (postViews) => {
    var chartDataArray = [];
    for(let i = 6; i >= 0; i--) {
      var curDate = new Date();
      curDate.setDate(curDate.getDate() - i);
      var amount = 0;
      for(let j = 0; j < postViews.length; j++)
      {
        var viewDate = new Date(postViews[j]);
        if(!(curDate.getDate() - viewDate.getDate())) {
          amount += 1;
        }
      }
      curDate = curDate.toDateString();
      chartDataArray.push({name: curDate, ilość: amount});
    }
    setChartData(chartDataArray);
  }

  return (
    <div
        className="container d-flex justify-content-center"
        style={{ "width": 900 }}
      >
        <div
            className="card m-4 p-4 rounded rounded-lg shadow border rounded-0"
            id="stats-container"
        >
            <div className="text-center">
                <h5>{post.post.title}</h5>
            </div>
            <BarChart width={600} height={300} data={chartData}>
              <XAxis dataKey="name" stroke="#8884d8" />
              <YAxis />
              <Tooltip />
              <Bar dataKey="ilość" fill="#8884d8" barSize={30} />
            </BarChart>
        </div>
    </div>
  );
};

export default PostStatisticsCard;
import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link, useParams } from "react-router-dom";
import { BarChart, Bar, XAxis, YAxis, Tooltip } from 'recharts';

const PostStatisticsCard = (postData) => {
  console.log(postData.postData.id);
  const [postViews, setPostViews] = useState([]);
  const [chartData, setChartData] = useState([]);

  useEffect(() => {
    (async () => {
      var result = await getPostViews();
      generateChartDataForWeek();
    })();
  }, []);

  const getPostViews = async () => {
    var result = await axios.get(`/api/Posts/list/views/${postData.postData.id}`);
    console.log(result);
    var postViews = result.data.model;
    var postViewsArray = [];
    postViews.map((postView) => {
      var viewDate = new Date(postView.viewDate);
      postViewsArray.push(viewDate);
    });
    var result = setPostViews(postViewsArray);
    return result;
  };

  const generateChartDataForWeek = async () => {
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

  const generateChartDataForMonth = async () => {
    var chartDataArray = [];
    for(let i = 4; i >= 1; i--) {
      var curDateLow = new Date();
      var curDateHigh = new Date();
      curDateLow.setDate(curDateLow.getDate() - i * 7);
      curDateHigh.setDate(curDateHigh.getDate() - (i-1) * 7);
      var amount = 0;
      for(let j = 0; j < postViews.length; j++)
      {
        var viewDate = new Date(postViews[j]);
        if(curDateLow < viewDate && viewDate < curDateHigh) {
          amount += 1;
        }
      }
      curDateLow = curDateLow.toDateString();
      curDateHigh = curDateHigh.toDateString();
      chartDataArray.push({name: `${curDateLow}-${curDateHigh}`, ilość: amount});
    }
    setChartData(chartDataArray);
  }

  const generateChartDataForYear = async () => {
    var chartDataArray = [];
    for(let i = 6; i >= 1; i--) {
      var curDateLow = new Date();
      var curDateHigh = new Date();
      curDateLow.setDate(curDateLow.getDate() - i * 30);
      curDateHigh.setDate(curDateHigh.getDate() - (i-1) * 30);
      var amount = 0;
      for(let j = 0; j < postViews.length; j++)
      {
        var viewDate = new Date(postViews[j]);
        if(curDateLow < viewDate && viewDate < curDateHigh) {
          amount += 1;
        }
      }
      curDateLow = curDateLow.toDateString();
      curDateHigh = curDateHigh.toDateString();
      chartDataArray.push({name: `${curDateLow}-${curDateHigh}`, ilość: amount});
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
                {console.log(postData.postData)}
                <h5>{postData.postData.title}</h5>
                <div class="btn-group" role="group" style={{"alignContent": "absolute"}}>
                  <button type="button" class="btn btn-secondary" onClick={generateChartDataForYear} style={{"font-size": 12}}>6 miesięcy</button>
                  <button type="button" class="btn btn-secondary" onClick={generateChartDataForMonth} style={{"font-size": 12}}>4 tygodnie</button>
                  <button type="button" class="btn btn-secondary" onClick={generateChartDataForWeek} style={{"font-size": 12}}>7 dni</button>
                </div>
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
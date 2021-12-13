import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Checkbox from "@mui/material/Checkbox";
import FormControlLabel from "@mui/material/FormControlLabel";

import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import { Link } from "react-router-dom";

const GroupBrowser = ({ abc, def }) => {
  const [groups, setGroups] = useState([]);
  const [topic, setTopic] = useState(null);
  const [name, setName] = useState("");
  const [isOpen, setIsOpen] = useState(false);

  const [topics, setTopics] = useState([]);

  const fetchGroups = async () => {
    const result = await axios.post("/api/Groups/find", {
      IsMember: true,
      topic: topic,
      name: name,
      isOpen: isOpen,
    });

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setGroups(result.data.data);
    }
  };

  const fetchTopics = async () => {
    const result = await axios.get("/api/Topics");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setTopics(result.data.data);
    }
  };

  useEffect(() => {
    fetchGroups();
    fetchTopics();
  }, [name, topic, isOpen]);

  return (
    <>
      <div className="container d-flex justify-content-center">
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          <div className="row mx-4 mb-4">
            <div style={{ width: "250px" }}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">Temat</InputLabel>
                <Select
                  size="small"
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={topic}
                  label="Temat"
                  onChange={(e) => {
                    setTopic(e.target.value);
                  }}
                >
                  {topics.map((t) => {
                    return (
                      <MenuItem key={t} value={t}>
                        {t}
                      </MenuItem>
                    );
                  })}
                </Select>
              </FormControl>
            </div>
            <div>
              <button
                type="button"
                className="btn ml-2 mt-1 btn-outline-secondary rounded-circle btn-sm"
                onClick={() => {
                  setTopic(null);
                }}
              >
                <i className="fa fa-times" aria-hidden="true"></i>
              </button>
            </div>
            <div className="mx-2" style={{ width: "300px" }}>
              <TextField
                fullWidth
                size="small"
                value={name}
                id="outlined-basic"
                label="Nazwa"
                variant="outlined"
                onChange={(e) => {
                  setName(e.target.value);
                }}
              />
            </div>

            <FormControlLabel
              className="mx-4"
              control={
                <Checkbox
                  checked={isOpen}
                  onChange={(e) => {
                    setIsOpen(e.target.checked);
                  }}
                />
              }
              label="Grupy otwarte"
            />
          </div>
          <TableContainer component={Paper}>
            <Table aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell width="200px"></TableCell>
                  <TableCell align="left">Nazwa</TableCell>
                  <TableCell align="left">Temat</TableCell>
                  <TableCell width="200px" align="center">
                    Liczba członków
                  </TableCell>
                  <TableCell width="200px" align="center">
                    Czy grupa otwarta?
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {groups.map((row) => (
                  <TableRow
                    key={row.name}
                    sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  >
                    <TableCell align="center">
                      <Link to={`/group/${row.id}`}>

                        <button
                          type="button"
                          className="btn btn-outline-primary rounded-circle btn-sm"
                        >
                          <i className="fa fa-sign-in" aria-hidden="true"></i>
                        </button>
                      </Link>
                    </TableCell>
                    <TableCell align="left">{row.name}</TableCell>
                    <TableCell align="left">{row.groupTopic}</TableCell>
                    <TableCell align="center">{row.membersCount}</TableCell>
                    <TableCell align="center">
                      {row.isOpen ? "Tak" : "Nie"}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </div>
      </div>
    </>
  );
};

export default GroupBrowser;

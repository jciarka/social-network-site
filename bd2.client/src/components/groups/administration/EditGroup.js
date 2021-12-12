import React, { useState, useEffect } from "react";
import axios from "axios";

import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";

const EditGroup = ({ group, setGroup, onCancel, onSuccess }) => {
  function validate() {
    return group.name && group.name !== "" && group.groupTopic;
  }

  const addOrUpdateGroup = async () => {
    if (!validate()) {
      console.log(`Not valid`);
      return;
    }
    let result;
    if (!group.id) {
      // Adding group
      console.log(`Adding group`);
      result = await axios.post(`/api/Groups/`, group);
    } else {
      // Updating group
      console.log(`Updating group`);
      result = await axios.put(`/api/Groups/${group.id}`, group);
    }

    if (result && result.data && result.data.success) {
      setGroup({});
      onSuccess();
    }
  };

  const [topics, setTopics] = useState([]);

  const fetchTopics = async () => {
    const result = await axios.get("/api/Topics");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setTopics(result.data.data);
    }
  }

  useEffect(() => {
    fetchTopics();
  }, []);

  return (
    <>
      <DialogTitle>Dane grupy</DialogTitle>
      <DialogContent style={{ minWidth: "450px" }}>
        <DialogContentText>
          <div className="p-1">
            <TextField
              className="my-2"
              size="small"
              fullWidth
              error={!group.name || group.name === ""}
              value={group.name}
              id="outlined-basic"
              label="Nazwa"
              variant="outlined"
              onChange={(e) => {
                setGroup({ ...group, name: e.target.value });
              }}
            />

            <FormControl className="my-2" fullWidth>
              <InputLabel size="small" id="demo-simple-select-label">
                Temat
              </InputLabel>
              <Select
                size="small"
                error={!group.groupTopic}
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={group.groupTopic}
                label="Temat"
                onChange={(e) => {
                  setGroup({ ...group, groupTopic: e.target.value });
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
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button color="error" onClick={onCancel}>
          Anuluj
        </Button>
        <Button disabled={!validate()} onClick={addOrUpdateGroup}>
          Zatwierdź
        </Button>
      </DialogActions>
    </>
  );
};

export default EditGroup;

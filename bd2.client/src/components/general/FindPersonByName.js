import React, { useState } from "react";
import axios from "axios";

import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Autocomplete from "@mui/material/Autocomplete";
import Box from "@mui/material/Box";
import reactDom from "react-dom";

const FindPersonByName = ({ onCancel, onSuccess }) => {
  const [open, setOpen] = React.useState(false);
  const [people, setPeople] = React.useState([]);
  const [person, setPerson] = React.useState({});

  const fetchPeopleByName = async (phrase) => {
    const result = await axios.get(`/api/accounts/findByName/${phrase}`);

    if (result && result.data && result.data.success) {
      setPeople(result.data.data);
    }
  };

  const handleConfirm = () => {
    if (onSuccess && person) {
      onSuccess(person)
    }
  }

  return (
    <>
      <DialogTitle>Znajdź użytkownika</DialogTitle>
      <DialogContent style={{ minWidth: "450px" }} fullWidth>
        <DialogContentText>
          <div className="p-1">
            <FormControl className="my-2 w-100" fullWidth>
              {/* <Autocomplete
                id="combo-box-demo"
                options={people}
                getOptionLabel={(option) =>
                  option.firstname + " " + option.lastname
                }
                renderInput={(params) => (
                  <TextField {...params} label="Combo box" variant="outlined" onInput={ (e) => {
                    if (e.target.value && e.target.value.length >= 3) {
                      fetchPeopleByName(e.target.value)
                    }                    
                  }}/>
                )}
              /> */}
              <Autocomplete
                fullWidth
                noOptionsText="Wpisz frazę"
                size="small"
                open={open}
                filter
                onOpen={() => {
                  setOpen(true);
                }}
                onClose={() => {
                  setOpen(false);
                }}
                isOptionEqualToValue={(option, value) => option.id === value.id}
                getOptionLabel={(option) =>
                  option.firstname + " " + option.lastname
                }
                options={people}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    label="Użytkownik"
                    InputProps={{
                      ...params.InputProps,
                    }}
                    onInput={(e) => {
                      if (e.target.value && e.target.value.length > 0)
                        fetchPeopleByName(e.target.value);
                    }}
                  />
                )}
                onChange={(event, newInputValue) => {
                  setPerson(newInputValue);
                }}
              />
            </FormControl>
          </div>
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button color="error" onClick={onCancel}>
          Anuluj
        </Button>
        <Button onClick={handleConfirm}>Zatwierdź</Button>
      </DialogActions>
    </>
  );
};

export default FindPersonByName;

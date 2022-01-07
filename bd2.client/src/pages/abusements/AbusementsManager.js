import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Dialog from "@mui/material/Dialog";
import PostCard from "components/post/PostCard";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";

import Button from "@mui/material/Button";
// import TextField from "@mui/material/TextField";
// import Checkbox from "@mui/material/Checkbox";
// import FormControlLabel from "@mui/material/FormControlLabel";

// import InputLabel from "@mui/material/InputLabel";
// import MenuItem from "@mui/material/MenuItem";
// import FormControl from "@mui/material/FormControl";
// import Select from "@mui/material/Select";
// import { Link } from "react-router-dom";

const AbusementsManager = ({}) => {
  const [abusements, setAbusements] = useState([]);
  const [post, setPost] = useState("");
  const [isOpen, setIsOpen] = useState(false);

  const [markedRow, setMarkedRow] = useState(null);
  const [isBlockOpen, setIsBlockOpen] = useState(false);
  const [isCancleOpen, setIsCancelOpen] = useState(false);

  const fetchAbusements = async () => {
    const result = await axios.get("/api/PostAbusements");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setAbusements(result.data.data);
    }
  };

  const fetchPost = async (id) => {
    const result = await axios.get(`/api/Posts/${id}`);

    if (result && result.data && result.data.success) {
      console.log(result.data.model);
      setPost(result.data.model);
    }
  };

  const executeAction = async (doBlock) => {
    const result = await axios.patch(`/api/PostAbusements/${markedRow.postId}/${markedRow.accountId}/Block/${doBlock}`);

    if (result && result.data && result.data.success) {
      setIsBlockOpen(false)
      setIsCancelOpen(false)
      fetchAbusements()
    }
  }

  useEffect(() => {
    fetchAbusements();
  }, []);

  const handleClose = () => {
    setIsOpen(false);
  };

  return (
    <>
      <Dialog fullScreen="true" open={isOpen && post} onClose={handleClose}>
        <div className="row justify-content-start mx-4 mt-4">
          <button
            type="button"
            className="btn btn-secondary rounded-pill btn-sm mx-1"
            onClick={handleClose}
          >
            <i className="fa fa-arrow-left" aria-hidden="true"></i> Powrót
          </button>
        </div>
        <div className="row justify-content-center">
          <PostCard postData={post} setPostData={setPost} />
        </div>
      </Dialog>
      <Dialog
        open={isBlockOpen && markedRow}
        onClose={() => {
          setIsBlockOpen(false);
        }}
      >
        <DialogTitle>Post jest nieprawidłowy</DialogTitle>
        <DialogContent style={{ minWidth: "450px" }}>
          <DialogContentText>
            Czy napewno chesz zablokować post. Jeśli zatwierdzisz, post nie będzie dłużej widoczny dla użytkowników.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button
            color="error"
            onClick={() => {
              setIsBlockOpen(false);
            }}
          >
            Anuluj
          </Button>
          <Button
            onClick={() => {
              executeAction(true);
            }}
          >
            Zatwierdź
          </Button>
        </DialogActions>
      </Dialog>

      <Dialog
        open={isCancleOpen && markedRow}
        onClose={() => {
          setIsCancelOpen(false);
        }}
      >
        <DialogTitle>Post jest prawidłowy</DialogTitle>
        <DialogContent style={{ minWidth: "450px" }}>
          <DialogContentText>
            Czy napewno chesz anulować zgłoszenie. Jeśli zatwierdzisz, zgłoszenie o nadużycie zostanie zignorowane.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button
            color="error"
            onClick={() => {
              setIsCancelOpen(false);
            }}
          >
            Anuluj
          </Button>
          <Button
            onClick={() => {
              executeAction(false);
            }}
          >
            Zatwierdź
          </Button>
        </DialogActions>
      </Dialog>

      <div className="container d-flex justify-content-center">
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          {/* <div className="row mx-4 mb-4">
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
          </div> */}
          <TableContainer component={Paper}>
            <Table aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell width="40px" align="center"></TableCell>
                  <TableCell align="center">Zgłosił</TableCell>
                  <TableCell align="center">Tytuł</TableCell>
                  <TableCell width="200px" align="center">
                    Przyczyna
                  </TableCell>
                  <TableCell align="left">Data zgłoszenia</TableCell>
                  <TableCell width="200px" align="center">
                    Akcje
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {abusements.map((row) => (
                  <TableRow
                    key={row.name}
                    sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  >
                    <TableCell align="center">
                      <button
                        type="button"
                        className="btn btn-outline-primary rounded-circle btn-sm"
                        onClick={() => {
                          fetchPost(row.postId);
                          setIsOpen(true);
                        }}
                      >
                        <i class="fa fa-search" aria-hidden="true"></i>
                      </button>
                    </TableCell>
                    <TableCell align="center">
                      {row.firstname} {row.lastname}
                    </TableCell>
                    <TableCell align="center">{row.postTitle}</TableCell>
                    <TableCell align="center">{row.text}</TableCell>
                    <TableCell align="center">
                      {row.abusementDate.slice(0, 19).split("T").join("  ")}
                    </TableCell>
                    <TableCell align="center">
                      <button
                        type="button"
                        className="btn btn-success rounded-circle btn-sm mx-1"
                        onClick={() => {
                          setMarkedRow(row)
                          setIsCancelOpen(true);
                        }}
                      >
                        <i class="fa fa-check" aria-hidden="true"></i>
                      </button>
                      <button
                        type="button"
                        className="btn btn-danger rounded-circle btn-sm mx-1"
                        onClick={() => {
                          setMarkedRow(row)
                          setIsBlockOpen(true);
                        }}
                      >
                        <i class="fa fa-ban" aria-hidden="true"></i>
                      </button>
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

export default AbusementsManager;

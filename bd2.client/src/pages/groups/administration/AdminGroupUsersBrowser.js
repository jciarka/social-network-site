import React, { useState, useEffect } from "react";
import FindPersonByName from "components/general/FindPersonByName";
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
import Dialog from "@mui/material/Dialog";

// import EditGroup from "components/groups/administration/EditGroup";
// import ChooseSubscription from "components/groups/administration/ChooseSubscription";

import { Link, useParams } from "react-router-dom";

const AdminGroupUsersBrowser = () => {
  const [open, setOpen] = React.useState(false);
  const [group, setGroup] = React.useState(null);

  const [members, setMembers] = React.useState([]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const { groupId } = useParams();

  const fetchGroup = async () => {
    const result = await axios.get(`/api/GroupAccounts/${groupId}`);
    if (result && result.data && result.data.success) {
      setMembers(result.data.data);
    }
  };

  const removePerson = async (userId) => {
    const result = await axios.delete(
      `/api/GroupAccounts/${groupId}/member/${userId}`
    );
    if (result && result.data && result.data.success) {
      await fetchGroup();
    }
  };

  const addPerson = async (userId) => {
    const result = await axios.post(`/api/groupaccounts`, {
      groupId: groupId,
      accountId: userId,
    });

    if (result && result.data && result.data.success) {
      await fetchGroup();
    }
  };

  useEffect(() => {
    fetchGroup();
  }, []);

  return (
    <>
      <Dialog open={open} onClose={handleClose}>
        <FindPersonByName
          onSuccess={(person) => {
            addPerson(person.id);
            handleClose();
          }}
          onCancel={handleClose}
        />
      </Dialog>

      <div className="container d-flex justify-content-center">
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          <div className="row mx-4 mb-4 align-items-center">
            <div className="row d-felx align-items-center w-100">
              <div className="d-flex" style={{ width: "100px" }}>
                <Link to={`/groups/administration`}>
                  <button
                    type="button"
                    className="btn ml-2 btn-outline-primary rounded-circle btn-sm"
                    onClick={() => {}}
                  >
                    <i class="fa fa-arrow-left" aria-hidden="true"></i>
                  </button>
                </Link>
                <h6 className="mx-2 mt-1">Powrót</h6>
              </div>
              <div className="justify-content-center text-center flex-fill">
                <h3>Członkowie grupy</h3>
              </div>
            </div>
          </div>

          <div className="row mx-4 mb-4 align-items-center">
            <button
              type="button"
              class="btn btn-primary rounded-circle align-items-center"
              onClick={() => {
                handleClickOpen();
              }}
            >
              <i class="fa fa-plus" aria-hidden="true"></i>
            </button>
            <h5 className="d-inline ml-2 pt-2">Dodaj członka</h5>
          </div>

          <TableContainer component={Paper}>
            <Table aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell width="50px"></TableCell>
                  <TableCell width="500px" align="left">
                    Imię i nazwisko
                  </TableCell>
                  <TableCell width="500px" align="center">
                    E-mail
                  </TableCell>
                  <TableCell width="100px" align="center"></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {members.map((row) => (
                  <TableRow
                    key={row.name}
                    sx={{
                      "&:last-child td, &:last-child th": { border: 0 },
                    }}
                  >
                    <TableCell align="center"></TableCell>
                    <TableCell align="left">
                      {row.firstname} {row.lastname}
                    </TableCell>
                    <TableCell align="center">{row.email}</TableCell>
                    <TableCell align="center">
                      {row.isAdmin && (
                        <i
                          class="fa fa-lock"
                          style={{ fontSize: "20px" }}
                          aria-hidden="true"
                        ></i>
                      )}
                      {!row.isAdmin && (
                        <button
                          type="button"
                          className="btn ml-2 btn-outline-danger rounded-circle btn-sm"
                          onClick={() => {
                            removePerson(row.accountId);
                          }}
                        >
                          <i class="fa fa-trash-o" aria-hidden="true"></i>
                        </button>
                      )}
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
export default AdminGroupUsersBrowser;

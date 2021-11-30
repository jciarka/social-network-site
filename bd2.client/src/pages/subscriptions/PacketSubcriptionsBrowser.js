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
import Dialog from "@mui/material/Dialog";
import AddSubcription from "components/subscriptions/AddSubscription";

import { Link } from "react-router-dom";

const PacketSubcriptionsBrowser = () => {
  //group administration
  const [open, setOpen] = React.useState(false);

  // filters
  const [subscriptions, setSubscriptions] = useState([]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const fetchSubscriptions = async () => {
    const result = await axios.get("/api/PacketSubscriptions");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setSubscriptions(result.data.data);
    }
  };

  useEffect(() => {
    fetchSubscriptions();
  }, []);

  return (
    <>
      <Dialog open={open} onClose={handleClose}>
        <AddSubcription
          onCancel={handleClose}
          onSuccess={() => {
            fetchSubscriptions();
            handleClose();
          }}
        />
      </Dialog>

      <div className="container d-flex justify-content-center">
        <div
          className="card m-4 p-4 rounded rounded-lg w-100 shadow border rounded-0"
          style={{ border: "#8f8f8fb6" }}
        >
          <div className="row mx-4 mb-4 align-items-center d-flex justify-content-between">
            <div style={{ width: "50px" }}>
              <button
                type="button"
                class="btn btn-primary rounded-circle"
                onClick={() => {
                  handleClickOpen();
                }}
              >
                <i class="fa fa-plus" aria-hidden="true"></i>
              </button>
            </div>
            <h2>Subskrypcje</h2>
            <div></div>
          </div>

          <TableContainer component={Paper}>
            <Table aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell width="200px"></TableCell>
                  <TableCell align="left">Nazwa</TableCell>
                  <TableCell width="200px" align="center">
                    Wolne sloty
                  </TableCell>
                  <TableCell width="200px" align="center">
                    Limit grup
                  </TableCell>
                  <TableCell width="200px" align="center">
                    Limit osób
                  </TableCell>
                  <TableCell width="200px" align="center">
                    Ważny do
                  </TableCell>
                  <TableCell width="200px" align="center">
                    Grupa otwarta
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {subscriptions.map((row) => (
                  <TableRow
                    key={row.name}
                    sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  >
                    <TableCell align="center">
                      <Button variant="contained">Przedłuż</Button>
                    </TableCell>
                    <TableCell align="left">{row.name}</TableCell>
                    <TableCell align="center">{row.freeSlots}</TableCell>
                    <TableCell align="center">{row.groupsLimit}</TableCell>
                    <TableCell align="center">{row.peopleLimit}</TableCell>
                    <TableCell align="center">
                      {row.expirationDate && row.expirationDate.split("T")[0]}
                    </TableCell>
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
export default PacketSubcriptionsBrowser;

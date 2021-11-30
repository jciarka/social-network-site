import React, { useState, useEffect } from "react";
import axios from "axios";

import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Checkbox from "@mui/material/Checkbox";
import FormControlLabel from "@mui/material/FormControlLabel";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Dialog from "@mui/material/Dialog";

const AddSubscription = ({ onCancel, onSuccess }) => {
  const [packet, setPacket] = useState(null);
  const [packets, setPackets] = useState([]);

  const buyPacket = async () => {
    console.log(`Chhosing subcription`);
    const result = await axios.post("/api/PacketSubscriptions", packet)

    if (result && result.data && result.data.success) {
      onSuccess();
    }
  };

  const fetchPackets = async () => {
    const result = await axios.get("/api/Packets");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setPackets(result.data.data);
    }
  };

  useEffect(() => {
    fetchPackets();
  }, []);

  return (
    <>
      <DialogTitle>Kup pakiet</DialogTitle>
      <DialogContent style={{ minWidth: "450px" }}>
        <DialogContentText>
          <div className="p-1">
            Uwaga! Transakca obciąży Twoje konto.
            <br /> 
            Czy na pewno chcesz kupić pakiet?
            <FormControl className="my-2" fullWidth>
              <InputLabel size="small" id="demo-simple-select-label">
                Temat
              </InputLabel>
              <Select
                size="small"
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={packets.id}
                label="Temat"
                onChange={(e) => {
                  setPacket({ packetId: e.target.value });
                }}
              >
                {packets.map((t) => {
                  return (
                    <MenuItem key={t.id} value={t.id}>
                      {t.name +
                        " | liczba grup: " +
                        t.groupsLimit +
                        " | limit osób: " +
                        t.peopleLimit +
                        " | liczba miesięcy: " +
                        t.packetPeriod +
                        " | cena: " +
                        t.price +
                        "zł"}
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
        <Button onClick={buyPacket}>Kup</Button>
      </DialogActions>
    </>
  );
};

export default AddSubscription;

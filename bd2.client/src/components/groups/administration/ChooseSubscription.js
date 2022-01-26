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

const ChooseSubscription = ({
  groupId,
  subscription,
  setSubscription,
  onCancel,
  onSuccess,
}) => {
  const [subcriptions, setSubcriptions] = useState([]);

  const putSubscription = async () => {
    console.log(`Chhosing subcription`);
    const result = await axios.put(
      `/api/Groups/${groupId}/subcription`,
      subscription
    );

    if (result && result.data && result.data.success) {
      setSubscription({});
      onSuccess();
    }
  };

  const fetchPackets = async () => {
    const result = await axios.get("/api/PacketSubscriptions");

    if (result && result.data && result.data.success) {
      console.log(result.data.data);
      setSubcriptions(result.data.data);
    }
  };

  useEffect(() => {
    fetchPackets();
  }, []);

  return (
    <>
      <DialogTitle>Wybierz subskrypcję</DialogTitle>
      <DialogContent style={{ minWidth: "450px" }}>
        <DialogContentText>
          <div className="p-1">
            <FormControl className="my-2" fullWidth>
              <InputLabel size="small" id="demo-simple-select-label">
                Nazwa
              </InputLabel>
              <Select
                size="small"
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={subscription.subcriptionId}
                label="Temat"
                onChange={(e) => {
                  setSubscription({ subscriptionId: e.target.value });
                }}
              >
                {subcriptions.map((t) => {
                  return (
                    <MenuItem key={t.id} value={t.id}>
                      {t.name +
                        " | wolnych slotów: " +
                        t.freeSlots +
                        " | ważny do: " +
                        (t &&
                          t.expirationDate &&
                          t.expirationDate.split("T")[0]) +
                        " | limit osób: " +
                        t.peopleLimit}
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
        <Button onClick={putSubscription}>Zatwierdź</Button>
      </DialogActions>
    </>
  );
};

export default ChooseSubscription;

import axios from "axios";

import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";


const AddAbusement = ({ abusement, setAbusement, onCancel, onSuccess }) => {
  function validate() {
    return abusement.text && abusement.text !== "" && abusement.postId;
  }

  const addAbusement = async () => {
    if (!validate()) {
      console.log(`Not valid`);
      return;
    }
    const result = await axios.post(`/api/Abusements/`, abusement);
    if (result && result.data && result.data.success) 
    {
      onSuccess();
    }
  };

  return (
    <>
      <DialogTitle>Przyczyna zgłoszenia</DialogTitle>
      <DialogContent style={{ minWidth: "450px" }}>
        <DialogContentText>
          <div className="p-1">
            <TextField
              className="my-2"
              size="small"
              fullWidth
              error={!(abusement.text && abusement.text !== "" && abusement.postId)}
              value={abusement.text}
              id="outlined-basic"
              label="Nazwa"
              variant="outlined"
              onChange={(e) => {
                setAbusement({ ...abusement, text: e.target.value });
              }}
            />
          </div>
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button color="error" onClick={onCancel}>
          Anuluj
        </Button>
        <Button disabled={!validate()} onClick={addAbusement}>
          Zatwierdź
        </Button>
      </DialogActions>
    </>
  );
};

export default AddAbusement;

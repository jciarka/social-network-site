import axios from "axios";

import Button from "@mui/material/Button";
import DialogActions from "@mui/material/DialogActions";
import DialogTitle from "@mui/material/DialogTitle";


const DeletePost = ({ postId, onCancel, onSuccess }) => {
    
  const deletePost = async () => {
    // const result = await axios.delete(`/api/Post/${postId}`);
    // if (result && result.data && result.data.success) 
    // {
    //   onSuccess();
    // }
  };

  return (
    <>
      <DialogTitle>Post zostanie bezpowrotnie usunięty</DialogTitle>
      <DialogActions>
        <Button color="error" onClick={onCancel}>
          Anuluj
        </Button>
        <Button onClick={deletePost}>
          Zatwierdź
        </Button>
      </DialogActions>
    </>
  );
};

export default DeletePost;

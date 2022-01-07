import axios from "axios";
import { useSelector } from "react-redux";

const ChatCard = ({ chatData, setChatData }) => {
  const account = useSelector((state) => state.account); // get redux store values
  const fetchChatsDetails = async () => {
    const result = await axios.get(`/api/Chat/${chatData.chat.id}`);

    if (result && result.data && result.data.success) {
      setChatData({
        ...chatData,
        detailsFetched: true,
        expanded: true,
        ...result.data.model,
      });
    }
  };
  
  return (
    <>
      <div
        className="card m-4 pt-0 pb-0 rounded rounded-lg w-100 shadow border border-dark rounded-0"
        style={{ border: "#8f8f8fb6", maxWidth: "800px" }}
      >
        {!chatData.isOwner && (
          <div className="mt-3" />
        )}
        <div style={{ marginBottom: 10 }} />
        <hr style={{ background: "black", marginBottom: 10, marginTop: 10 }} />
        <div className="h5  mx-4  justify-content-center text-center">
          {chatData.chat.title}
        </div>

      </div>
    </>
  );
};

export default ChatCard;

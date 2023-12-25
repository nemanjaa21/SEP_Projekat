import { toast } from "react-toastify";
import { bitcoinPayment } from "services/PSPService";
import Web3 from "web3";

export const getImageLink = (imageName) => {
    if (imageName) return process.env.REACT_APP_AUTH_URL + "/user/image/" + imageName;
    return "default.jpg";
  };

export const createEthereumPayment = async (id) => {
    let orderId = -1;
    try {
      if (!window.ethereum || window.ethereum === undefined) {
        toast.error("Please install MetaMask and login");
        return;
      }
  
      await window.ethereum.request({ method: "eth_requestAccounts" });
      const web3 = new Web3(window.ethereum);
  
      const accs = await web3.eth.getAccounts();
      if (accs.length === 0) {
        toast.error("Please login to MetaMask and make account");
        return;
      }
  
      const transactionData = await api.post(`/payment/ethereum/create/${id}`);
      orderId = transactionData.data.orderId;
      const response = await web3.eth.sendTransaction({
        from: accs[0],
        ...transactionData.data,
      });
  
      if (response.status === 0) {
        toast.error("Something went wrong, try again or different way");
        return;
      }
  
      await api.get(`/payment/ethereum/check/${response.transactionHash}`);
      toast.success("Payment is successful");
      setTimeout(() => {
        window.location.reload();
      }, 2000);
    } catch (e) {
      console.log(e);
      if (orderId !== -1) await api.get(`/payment/ethereum/cancel/${orderId}`);
      toast.error("Please connect to MetaMask and try again");
    }
  };

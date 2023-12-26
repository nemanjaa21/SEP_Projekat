import { pspApi, paypalApi } from "../helpers/ConfigHelper";

export const payPalPayment = async (serviceOffer) => {
  return await paypalApi.post(`/PayPalPayment/payment`, serviceOffer);
};

export const processPayment = async (pspRequest, apiKey) => {
  return await pspApi.post(`PaymentService/process-payment?apiKey=${apiKey}`, pspRequest);
}

import { pspApi } from "../helpers/ConfigHelper";

export const qrCodePayment = async () => {
  return await pspApi.get(`/qrcode-payment`);
};

import { bankApi } from "../helpers/ConfigHelper";

export const payWithCard = async (creditCardInfo) => {
  return await bankApi.post(`/Banks/pay-with-card`, creditCardInfo);
};
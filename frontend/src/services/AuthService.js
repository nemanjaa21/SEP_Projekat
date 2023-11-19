import api from "../helpers/ConfigHelper";

export const login = async (logInData) => {
  return await api.post(`/Auth`, logInData);
};
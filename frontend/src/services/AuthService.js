import { authApi } from "../helpers/ConfigHelper";

export const login = async (logInData) => {
  return await authApi.post(`/Auth/login`, logInData);
};
import axios from "axios";

const authApi = axios.create({
    baseURL: process.env.REACT_APP_AUTH_URL,
    headers: {
        'Content-Type': 'application/json',
        'ngrok-skip-browser-warning': '1'
    },
});

const agencyApi = axios.create({
    baseURL: process.env.REACT_APP_AGENCY_URL,
    headers: {
        'Content-Type': 'application/json',
        'ngrok-skip-browser-warning': '1'
    },
});

const pspApi = axios.create({
    baseURL: process.env.REACT_APP_PSP_URL,
    headers: {
        'Content-Type': 'application/json',
        'ngrok-skip-browser-warning': '1'
    },
});

const paypalApi = axios.create({
    baseURL: "https://localhost:7140/api",
    headers: {
        'Content-Type': 'application/json',
    },
});

const bankApi = axios.create({
    baseURL: process.env.REACT_APP_BANK_URL,
    headers: {
        'Content-Type': 'application/json',
    },
})

const bitcoinApi = axios.create({
    baseURL: process.env.REACT_APP_BITCOIN_URL,
    headers: {
        'Content-Type': 'application/json',
    },
})

authApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
});

agencyApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
}); 

pspApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
}); 

paypalApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
}); 

bankApi.interceptors.request.use((config) => { 

    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
});

bitcoinApi.interceptors.request.use((config) => { 
    try{ 
        const token = sessionStorage.getItem('token');
        if(token){ 
            return {...config, headers: { 
                ...config.headers, 
                Authorization: `Bearer ${token}`, 
            }};
        } 
        return config; 
    } catch(e) { 
        console.log(e); 
        return Promise.reject(e); 
    } 
});

export { authApi, agencyApi, pspApi, bankApi, paypalApi, bitcoinApi };

import { useState } from 'react';
import { payWithCard } from '../../services/BankService';

const CardForm = () => {
  const [formData, setFormData] = useState({
    Pan: '',
    SecurityCode: '',
    CardHolderName: '',
    ExpirationDate: '',
    PaymentId: sessionStorage.getItem("paymentId"),
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await payWithCard(formData);
      window.location.href = response.data.url;
      console.log(response.data);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        name="Pan"
        value={formData.Pan}
        placeholder="Card Number"
        onChange={handleChange}
      />
      <input
        type="text"
        name="SecurityCode"
        value={formData.SecurityCode}
        placeholder="Security Code"
        onChange={handleChange}
      />
      <input
        type="text"
        name="CardHolderName"
        value={formData.CardHolderName}
        placeholder="Cardholder's Name"
        onChange={handleChange}
      />
      <input
        type="text"
        name="ExpirationDate"
        value={formData.ExpirationDate}
        placeholder="Expiration Date"
        onChange={handleChange}
      />
      <button type="submit">Submit</button>
    </form>
  );
};

export default CardForm;
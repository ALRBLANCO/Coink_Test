namespace UserRegistrationApi.src.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public int CityId { get; private set; }

    public User(string fullName, string phone, string address, int cityId)
    {
        // El dominio valida sus propias reglas invariantes
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Nombre obligatorio");
        
        FullName = fullName;
        Phone = phone;
        Address = address;
        CityId = cityId;
    }

    // Aquí podrías agregar métodos de comportamiento
    public void UpdateAddress(string newAddress) 
    {
        Address = newAddress;
    }
}
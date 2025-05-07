using UnityEngine;
using TMPro;

public class PatientInformation : MonoBehaviour
{
    [Header("Patient Details")]
    [SerializeField] private string patientName = "John Doe";
    [SerializeField, TextArea(3,10)] private string patientDescription = "Patient presents with...";
    
    [Header("Vital Signs")]
    [SerializeField] private string bloodPressure = "120/80";
    [SerializeField] private string heartRate = "72";
    [SerializeField] private string respiratoryRate = "16";
    [SerializeField] private string temperature = "37.0";
    [SerializeField] private string spO2 = "98";

    public string GetPatientName() => patientName;
    public string GetPatientDescription() => patientDescription;

    public string[] GetVitalSigns()
    {
        return new string[] {
            $"Blood Pressure: {bloodPressure} mmHg",
            $"Heart Rate: {heartRate} bpm",
            $"Respiratory Rate: {respiratoryRate} /min",
            $"Temperature: {temperature}°C",
            $"SpO2: {spO2}%"
        };
    }
}
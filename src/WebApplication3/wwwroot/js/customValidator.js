/* js/customValidator.js */

$.extend($.validator.messages, {
  required: "To polje je obvezno.",
  remote: "Prosimo popravite to polje.",
  email: "Prosimo vnesite veljaven email naslov.",
  url: "Prosimo vnesite veljaven URL naslov.",
  date: "Prosimo vnesite veljaven datum.",
  dateISO: "Prosimo vnesite veljaven ISO datum.",
  number: "Prosimo vnesite veljavno �tevilo.",
  digits: "Prosimo vnesite samo �tevila.",
  creditcard: "Prosimo vnesite veljavno �tevilko kreditne kartice.",
  equalTo: "Prosimo ponovno vnesite vrednost.",
  extension: "Prosimo vnesite vrednost z veljavno kon�nico.",
  maxlength: $.validator.format("Prosimo vnesite najve� {0} znakov."),
  minlength: $.validator.format("Prosimo vnesite najmanj {0} znakov."),
  rangelength: $.validator.format("Prosimo vnesite najmanj {0} in najve� {1} znakov."),
  range: $.validator.format("Prosimo vnesite vrednost med {0} in {1}."),
  max: $.validator.format("Prosimo vnesite vrednost manj�e ali enako {0}."),
  min: $.validator.format("Prosimo vnesite vrednost ve�je ali enako {0}.")
});
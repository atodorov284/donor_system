USE `donor_system`;
INSERT INTO donors (name, email, password, phoneNumber, bloodGroup)
VALUES 
("Petur Stoqnov", "petur.stoqnov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "A+"),
("Dimitur Petrov", "dimitur.petrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "0+"),
("Stefan Stoqnov", "stefan.stoqnov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "0-"),
("Teodora Stoqnova", "teodora.stoqnova@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "B-"),
("Pesho Petrov", "pesho.petrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234","0+");


INSERT INTO websites(name, description)
VALUES
("https://www.blood.co.uk/", "Explore being a donor, the donation process and centres where you can give blood in the UK."),
("https://www.redcrossblood.org/", "Helping others in need just feels good. Donate blood today."),
("https://www.friends2support.org/", "World's Largest Voluntary Blood Donors Database");
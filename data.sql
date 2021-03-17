USE `donor_system`;
INSERT INTO donors (name, email, password, phoneNumber, status, bloodGroup)
VALUES 
("Petur Stoqnov", "petur.stoqnov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "Available", "A+"),
("Dimitur Petrov", "dimitur.petrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "Available", "0+"),
("Stefan Stoqnov", "stefan.stoqnov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "Available", "0-"),
("Teodora Stoqnova", "teodora.stoqnova@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "Available", "B-"),
("Pesho Petrov", "pesho.petrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "0881231234", "Available", "0+");

INSERT INTO patients (name, email, password, diagnose, bloodGroup, phoneNumber)
VALUES
("Dimitur Dimitrov", "dimitur.dimitrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "Blood Cancer", "A+", "0881231234"),
("Nikolai Petrov", "nikolai.petrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "Blood Cancer", "A-", "0881231234"),
("Petq Stoqnova", "petq.stoqnova@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "Blood Cancer", "0+", "0881231234"),
("Dimitur Dimitrov", "dimitur.dimitrov@abv.bg", "btWDPPNShuv4Zit7WUnw10K77D8=", "Blood Cancer", "A+", "0881231234");


INSERT INTO websites(name, description)
VALUES
("https://www.blood.co.uk/", "Explore being a donor, the donation process and centres where you can give blood in the UK."),
("https://www.redcrossblood.org/", "Helping others in need just feels good. Donate blood today."),
("https://www.friends2support.org/", "World's Largest Voluntary Blood Donors Database");
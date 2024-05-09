-- Update password to "P2rmis123@"
update [user] set password='C5CBF0DCE3BD5C680B402B3A32A48C6F5D3B3C0C', 
passwordsalt='5We3HgZbvRDqIi8mrX+wdpnxvEg8T69ElRLg18ALzDI=' where [password] is not null
-- Update emails to fake emails
update useremail set email = email + '.fake' where email<>'' and email not like '%.fake'
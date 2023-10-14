CREATE OR REPLACE FUNCTION main.guid_trigger_func()
	RETURNS trigger
	LANGUAGE 'plpgsql'
	COST 100
	STABLE NOT LEAKPROOF
AS $BODY$
BEGIN
	IF(TG_OP = 'UPDATE') THEN 
	-- do not allow users to change the guid once it has been set
		IF(NEW.guid <> OLD.guid) THEN
			NEW.guid = OLD.guid;
		END IF;
	END IF;
	RETURN NEW;
END;
$BODY$;

COMMENT ON FUNCTION main.guid_trigger_func()
	IS 'Prevents the guid from being changed';
		

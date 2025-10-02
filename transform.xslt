<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="Pay">
    <Employees>
      <xsl:for-each select="descendant-or-self::item">
        <Employee>
          <xsl:attribute name="name">
            <xsl:value-of select="@name"/>
          </xsl:attribute>
          <xsl:attribute name="surname">
            <xsl:value-of select="@surname"/>
          </xsl:attribute>
            <salary>
              <xsl:attribute name="amount">
                <xsl:value-of select="@amount"/>
              </xsl:attribute>
              <xsl:attribute name="mount">
                <xsl:value-of select="@mount"/>
              </xsl:attribute>
            </salary>
        </Employee>
      </xsl:for-each>
    </Employees>
  </xsl:template>
</xsl:stylesheet>
